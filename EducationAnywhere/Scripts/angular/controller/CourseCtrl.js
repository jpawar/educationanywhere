myApp.controller('CourseCtrl', function($scope, $http) {

    var url = "/Course/Create";
    
    $scope.createCourse = function() {
        
        var courseInfo = $.param({
            Subject: $scope.course.name,
            Grade: $scope.course.grade
        });

        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: courseInfo
        }).success(courseSuccess).error(courseFailed);

        function courseSuccess(data, status, headers, config) {            
            window.location.href = '/Course/Index';
        }

        function courseFailed(data, status, headers, config) {
            
        }

    };
})