(function () {

    var mainChart = angular.module('MainChart', [
        "ngAnimate",
        'nvd3',
        'blockUI'
    ]);

    //service
    mainChart.service('chartDataSearch', function ($http) {
        this.getSearchChartList = function (domCd, beforeDay) {
            var formdata = {
                domCd: domCd,
                beforeDay: beforeDay
            }

            return $http({
                method: 'POST',
                url: '/Main/SearchOrderChart',
                data: JSON.stringify(formdata),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(
                function successCallback(response) {
                    return response
                },
                function errorCallback(response) {
                    alert("error");
                }
            );
        };

    });

    mainChart.controller('ChartCtrl', ['$scope', 'chartDataSearch', function ($scope, chartDataSearch) {
        $scope.options = {
            chart: {
                type: 'lineChart',
                width: 1800,
                height: 450,
                margin: {
                    top: 20,
                    right: 20,
                    bottom: 60,
                    left: 60
                },
                x: function (d) { return d.Key },
                y: function (d) { return d.Value },

                color: d3.scale.category10().range(),
                useInteractiveGuideline: true,
                clipVoronoi: false,
                showControls: false,

                xAxis: {
                    ticks: 12,
                        
                    tickFormat : function(d) {
                        return d3.time.format('%y/%m/%d')(new Date(d));
                    },
                },

                yAxis: {
                    axisLabel: 'OrderCount',
                    tickFormat: function (d) {
                        return d3.format('d')(d);
                    },
                },
            }
        };

        $scope.getSearchChart = function (blockUI, beforDay) {
            blockUI.start();
            var syncOrderlist = chartDataSearch.getSearchChartList($scope.domCd, beforDay);
            syncOrderlist.then(function (resp) {
                if (resp) {
                    var jsonResult = JSON.parse(resp.data);
                    if (jsonResult.Success) {
                        if (jsonResult.List.length > 0) {
                            if ($scope.domCd === "HF")
                                $scope.chartDataHf = jsonResult.List;
                            else if ($scope.domCd === "GA")
                                $scope.chartDataGA = jsonResult.List;
                            else if ($scope.domCd === "BR")
                                $scope.chartDataBR = jsonResult.List;
                            else
                                alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            }, function () {
                alert("error");
            }).finally(function () {
                blockUI.stop();
            });
        }
    }]);

    mainChart.controller("ChartCtrlHF", ['$scope', '$controller', 'blockUI', function ($scope, $controller, blockUI) {
        var BlockHf = blockUI.instances.get('chartBlockHF');
        $scope.domCd = "HF";
        $scope.beforeDay = "30";
        $controller('ChartCtrl', { $scope: $scope });

        $scope.getDdlSearchChart = function (beforeDay) {
            $scope.beforeDay = beforeDay;
            $scope.getSearchChart(BlockHf, $scope.beforeDay);
        };

        $scope.getSearchChart(BlockHf, $scope.beforeDay);

    }]);

    mainChart.controller("ChartCtrlGA", ['$scope', '$controller', 'blockUI', function ($scope, $controller, blockUI) {
        var BlockGa = blockUI.instances.get('chartBlockGA');
        $scope.domCd = "GA";
        $scope.beforeDay = "30";
        $controller('ChartCtrl', { $scope: $scope });

        $scope.getDdlSearchChart = function (beforeDay) {
            $scope.beforeDay = beforeDay;
            $scope.getSearchChart(BlockGa, $scope.beforeDay);
        };

        $scope.getSearchChart(BlockGa, $scope.beforeDay);
    }]);

    mainChart.controller("ChartCtrlBR", ['$scope', '$controller', 'blockUI', function ($scope, $controller, blockUI) {
        var BlockBr = blockUI.instances.get('chartBlockBR');
        $scope.domCd = "BR";
        $scope.beforeDay = "30";
        $controller('ChartCtrl', { $scope: $scope });

        $scope.getDdlSearchChart = function (beforeDay) {
            $scope.beforeDay = beforeDay;
            $scope.getSearchChart(BlockBr, $scope.beforeDay);
        };

        $scope.getSearchChart(BlockBr, $scope.beforeDay);
    }]);
})();