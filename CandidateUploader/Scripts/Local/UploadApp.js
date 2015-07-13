(function () {

    'use strict';


    // create the new app to handle the upload of the files
    var app = angular.module('uploadApp', ['angularFileUpload']);


    // create a directive to handle the tag plugin
    app.directive('tagDirective', function() {

        return {
            restrict: 'A',
            link: function(scope, element) {

                // init the tab box
                $(element).tagging();

            }
        };

    });


    // create the controller
    app.controller('UploadCtrl', ['$scope', '$upload', '$http', function ($scope, $upload, $http) {

        $scope.maxImageByte = 0;
        $scope.percentage = 0;
        $scope.success = false;
        $scope.process = false;
        $scope.error = '';
        $scope.data = {};
        $scope.files = [];
        $scope.upload = [];


        // function to store the selected files to upload
        $scope.onFileSelect = function ($files) {

            $scope.files = [];

            angular.forEach($files, function (file) {


                // test for image files and check the file dimension
                if (/^(image)\/([\w]+)$/.test(file.type)) {

                    if (file.size <= $scope.maxImageByte)

                        this.push(file);

                    else

                        $scope.error = "La dimensione dell'immagine supera il limite di " + Math.round($scope.maxImageByte / 1024 / 1024) + " MB!";

                }


                // test for video files
                if (/^(video)\/([\w]+)$/.test(file.type)) {

                    this.push(file);

                }


            }, $scope.files);

        };


        // function that upload the files to the server
        $scope.uploadFiles = function() {


            // activate the process
            $scope.process = true;


            // loop all the files
            angular.forEach($scope.files, function(file) {


                // for the videos, before check the duration
                if (/^(video)\/([\w]+)$/.test(file.type)) {

                    $scope.getVideoHeader(file);

                } else {

                    $scope.uploadFile(file);

                }


            });


        };



        // function that upload one file to the server
        $scope.uploadFile = function (file) {


            $scope.upload.push(

                // upload the file
                $upload.upload({
                    url: "/Upload/UploadFile",
                    method: "POST",
                    data: {
                        data: {
                            Title: $scope.data.title,
                            Author: $scope.data.author,
                            Email: $scope.data.email,
                            Location: $scope.data.location,
                            FreeTags: JSON.stringify(angular.element('#freeTagsBox').tagging('getTags')),
                            FileType: file.type,
                            ContestId: $scope.data.contest
                        }
                    },
                    file: file
                }).progress(function (evt) {

                    // get upload percentage
                    $scope.percentage = parseInt(100.0 * evt.loaded / evt.total);
                    console.log('percent: ' + $scope.percentage);

                }).success(function (data) {

                    // file is uploaded successfully
                    $scope.success = true;
                    $scope.process = false;
                    console.log(data);

                }).error(function (data) {

                    // file failed to upload
                    $scope.process = false;
                    $scope.success = false;
                    console.log(data);

                })
            );


        };


        $scope.getVideoHeader = function (file) {


            var start = parseInt(0) || 0;
            var stop = parseInt(100000) || file.size - 1;

            var reader = new FileReader();

            // If we use onloadend, we need to check the readyState.
            reader.onloadend = function (evt) {
                
                if (evt.target.readyState == FileReader.DONE) { // DONE == 2
                    
                    //console.log(evt.target.result);
                    console.log(['Read bytes: ', start + 1, ' - ', stop + 1,
                        ' of ', file.size, ' byte file'].join(''));


                    var bytes = '';

                    for (var i = 0; i < evt.target.result.length; ++i) {
                        bytes += evt.target.result.charCodeAt(i) + ' ';
                    }

                    var data = { file: bytes, filename: file.name };
                    

                    $http({ method: 'POST', url: '/Upload/VideoAllowed', data: data }).success(function (videoAllowed) {

                        console.log('video: ' + videoAllowed);

                        if (videoAllowed.Result == 1) {

                            $scope.uploadFile(file);

                        } else {

                            $scope.process = false;
                            $scope.error = "Il video ha una durata maggiore di " + videoAllowed.MaxSeconds + " secondi";

                        }

                    });

                }
            };

            var blob = file.slice(start, stop + 1);
            reader.readAsBinaryString(blob);

        };


        // function to abort the upload of a file
        $scope.abortUpload = function(index) {
            $scope.upload[index].abort();
        };


    }]);

}());