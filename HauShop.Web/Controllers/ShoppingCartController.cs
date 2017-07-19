using AutoMapper;
using HauShop.Common;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.App_Start;
using HauShop.Web.Infrastructure.Extensions;
using HauShop.Web.Infrastructure.NganluongAPI;
using HauShop.Web.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HauShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private ApplicationUserManager _userManager;

        private string merchanId = ConfigHelper.GetByKey("MerchanId");
        private string merchanpassword = ConfigHelper.GetByKey("Merchanpassword");
        private string merchanemail = ConfigHelper.GetByKey("Merchanemail");

        public ShoppingCartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._userManager = userManager;
            this._orderService = orderService;
        }

        //GetUser
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public ActionResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);

            var orderNew = new Order();
            orderNew.UpdateOrder(order);

            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            bool isEnough = true;
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;
                orderDetails.Add(detail);

                isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                break;
            }

            if (isEnough)
            {
                var orderReturn = _orderService.Create(orderNew, orderDetails);
                _productService.Save();

                if (order.PaymentMethod == "CASH")
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    var currentLink = ConfigHelper.GetByKey("CurrentLink");

                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = merchanId;
                    info.Merchant_password = merchanpassword; // "matkhauketnoi";
                    info.Receiver_email = merchanemail; // "demo@nganluong.vn";

                    info.cur_code = "vnd";
                    info.bank_code = order.BankCode;

                    info.Order_code = orderReturn.ID.ToString();
                    info.Total_amount = orderDetails.Sum(x => x.Quantity * x.Price).ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toan đơn hàng tại HauShop";
                    info.return_url = currentLink + "xac-nhan-don-hang.html";
                    info.cancel_url = currentLink + "huy-don-hang.html";

                    info.Buyer_fullname = order.CustomerName;
                    info.Buyer_email = order.CustomerEmail;
                    info.Buyer_mobile = order.CustomerMobile;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, order.PaymentMethod);

                    if (result.Error_code == "00")
                    {
                        return Json(new
                        {
                            status = true,
                            urlCheckout = result.Checkout_url
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false,
                            message = result.Description
                        });
                    }
                }
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Không đủ hàng"
                });
            }
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            var product = _productService.GetById(productId);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Sản phẩm này hiện đang hết hàng"
                });
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }

        public ActionResult ConfirmOrder()
        {
            string token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = merchanId;
            info.Merchant_password = merchanpassword;
            info.Token = token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if(result.errorCode == "00")
            {
                _orderService.UpdateStatus(int.Parse(result.order_code));
                _orderService.Save();
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Thanh toán thành công. chúng tôi sẽ liên hệ sớm nhất.";
            }
            else
            {
                ViewBag.IsSuccess = false;
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin";
            }
            return View();
        }

        public ActionResult CancelOrder()
        {
            return View();
        }
    }
}