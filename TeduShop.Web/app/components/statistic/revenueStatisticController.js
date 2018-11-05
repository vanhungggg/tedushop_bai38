(function (app) {
    app.controller('revenueStatisticController', revenuStatisticController);

    revenuStatisticController.$inject = ['apiService', '$scope', 'notificationService','$filter'];

    function revenuStatisticController(apiService, $scope, notificationService, $filter) {
        $scope.tabledata = [];

        $scope.labels = [];
        $scope.series = ['Doanh SỐ', 'Lợi nhuận'];

        $scope.chartdata = [];

        function getStatistic() {
            var config = {
                params: {
                    fromDate: '01/01/2016',
                    toDate: '01/01/2019'
                }
            }
            //apiService.get('api/statistic/getrevenue?fromDate='+config.params.fromDate+"&toDate="+config.params.toDate, null, function (response) {
            apiService.get('api/statistic/getrevenue', config, function (response) {
                $scope.tabledata = response.data;
                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefits = [];
                $.each(response.data, function (i, item) {
                    labels.push($filter('date')(item.Date,'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    benefits.push(item.Benefit);
                });
                //console.log(labels);
                chartData.push(revenues);
                chartData.push(benefits);
                //console.log(chartData);
                $scope.chartdata = chartData;
                $scope.labels = labels;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu');
            });
        }
        getStatistic();
    }

})(angular.module('tedushop.statistics'));