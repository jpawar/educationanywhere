myApp.controller('TutorialCtrl', function ($scope, $http, $upload) {

    var fileName = '';
    
    $scope.getCourses = function () {                
        $http.get('/Tutorial/Course').then(getCourseSuccess);
    };

    $scope.uploadTutorial = function() {
        
        var tutorialUpload = $.param({
            Subject: $scope.selectedcourse,
            Description: $scope.description,
            Grade: $scope.selectedgrade,
            FullFilePath: fileName
        });

        $http({
            method: 'POST',
            url: '/Tutorial/Save',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: tutorialUpload
        }).success(redirectToCourse).error(showErrorMessage);
    };

    function redirectToCourse(data, status, headers, config) {        
        window.location.href = '/Course/Index';
    }

    $scope.onFileSelect = function($files) {
        //$files: an array of files selected, each file has name, size, and type.
        for (var i = 0; i < $files.length; i++) {
            var $file = $files[i];
            $scope.upload = $upload.upload({
                url: '/tutorial/upload', //upload.php script, node.js route, or servlet url
                // method: POST or PUT,
                // headers: {'headerKey': 'headerValue'}, withCredential: true,
                data: { myObj: $scope.myModelObj },
                file: $file,
                //(optional) set 'Content-Desposition' formData name for file
                //fileFormDataName: myFile,
                progress: function(evt) {
                    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                }
            }).success(function(data, status, headers, config) {
                // file is uploaded successfully
                fileName = data;
                console.log(data);
            });
            //.error(...).then(...); 
        }
    };

    function getCourseSuccess(data) {
        $scope.CourseList = data.data;
    }
    
});