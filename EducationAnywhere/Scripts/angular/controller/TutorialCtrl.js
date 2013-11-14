myApp.controller('TutorialCtrl', function ($scope, $http, $upload) {

    var url = "/Tutorial";
    var fileName = '';
    
    $scope.getCourses = function () {                
        $http.get('/Tutorial/Course').then(getCourseSuccess);
    };

    $scope.uploadTutorial = function() {
        alert($scope.selectedgrade);
        alert($scope.selectedcourse);
        alert($scope.description);
        alert(fileName);
    };

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


//var FileUploadCtrl = ['$scope', '$upload', function ($scope, $upload) {
//    $scope.onFileSelect = function($files) {
//        //$files: an array of files selected, each file has name, size, and type.
//        for (var i = 0; i < $files.length; i++) {
//            var $file = $files[i];
//            $scope.upload = $upload.upload({
//                url: '/tutorial/upload', //upload.php script, node.js route, or servlet url
//                // method: POST or PUT,
//                // headers: {'headerKey': 'headerValue'}, withCredential: true,
//                data: { myObj: $scope.myModelObj },
//                file: $file,
//                //(optional) set 'Content-Desposition' formData name for file
//                //fileFormDataName: myFile,
//                progress: function(evt) {
//                    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
//                }
//            }).success(function(data, status, headers, config) {
//                // file is uploaded successfully
//                console.log(data);
//            });
//            //.error(...).then(...); 
//        }
//    };
//}];