/// <reference path="\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter','commonService'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter, commonService) {
        $scope.products = [];
        $scope.getProducts = getProducts;

        $scope.parentCategories = []
        $scope.flatFolders = []
        $scope.loadParentCategories = loadParentCategories

        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.keyword = '';

        $scope.loading = true

        $scope.search = search;

        $scope.deleteProduct = deleteProduct

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        $scope.exportExel = exportExel;
        $scope.exportPdf = exportPdf;

        function exportExel() {
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            apiService.get('/api/product/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error)
            })
        }

        function exportPdf(productId) {
            var config = {
                params: {
                    id: productId
                }
            }
            apiService.get('/api/product/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error)
            })
        }

        //deleteMultiple
        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc chắc muốn xóa bản ghi đã chọn ?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                })
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/product/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi')
                    search()
                }, function (error) {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        }

        //selectAll
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.isAll = false;

        //listening use watch
        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        //deleteProductCategory
        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công')
                    search()
                }, function () {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        };

        //search
        function search() {
            $scope.loading = true
            //alert($scope.productCategory.ParentID)
            getProducts($scope.page, $scope.productCategory.ParentID);
        }

        //getProductCategories
        function getProducts(page, category) {
            var config = {
                params: {
                    keyword: $scope.keyword,
                    category: category, 
                    page: page || 0,
                    pagesize: 12
                }
            }
            //alert($scope.productCategory.ParentID)
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('Không có bản ghi nào');
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

                $scope.loading = false

            }, function (error) {
                console.log('load product category failed. ');
            });
        }

        //loadParentCategories
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentCategories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                })
            }, function (error) {
                console.log('cannot get list parent.');
            })
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };


        //load
        $scope.loadParentCategories()
        $scope.getProducts($scope.page,0);
    }
})(angular.module('haushop.products'))