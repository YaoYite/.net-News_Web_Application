// Defining angularjs module
var app = angular.module('demoModule', []);

// Defining angularjs Controller and injecting ProductsService
app.controller('demoCtrl', function ($scope, $http, ArticleService) {

    $scope.articleData = null;
    // Fetching records from the factory created at the bottom of the script file
    ArticleService.GetAllRecords().then(function (d) {
        $scope.articleData = d.data; // Success
    }, function () {
        alert('Error Occured !!!'); // Failed
    });

    $scope.Article = {
        ArticleID: '',
        title: '',
        datePub: '',
        description: '',
        comments: '',
        topic: '',
    };

    // Reset product details
    $scope.clear = function () {
        $scope.Article.ArticleID = '';
        $scope.Article.title = '';
        $scope.Article.datePub = '';
        $scope.Article.description = '';
        $scope.Article.comments = '';
        $scope.Article.topic = '';
    }

    //Add New Item
    $scope.save = function () {
        if ($scope.Article.ArticleID != "" &&
       $scope.Article.title != "" && $scope.Article.datePub != "" && $scope.Article.topic != "") {
            // Call Http request using $.ajax

            //$.ajax({
            //    type: 'POST',
            //    contentType: 'application/json; charset=utf-8',
            //    data: JSON.stringify($scope.Book),
            //    url: 'api/Book/PostBook',
            //    success: function (data, status) {
            //        $scope.$apply(function () {
            //            $scope.bookData.push(data);
            //            alert("Book Added Successfully !!!");
            //            $scope.clear();
            //        });
            //    },
            //    error: function (status) { }
            //});

            // or you can call Http request using $http
            $http({
                method: 'POST',
                url: 'api/Article/PostArticle/',
                data: $scope.Article
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.articleData.push(response.data);
                $scope.clear();
                alert("Article Added Successfully !!!");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Edit product details
    $scope.edit = function (data) {
        $scope.Article = { Id: Article.ArticleID, ArticleTitle: Article.title, Dateofpublish: Article.datePub, Description: Article.description, Numberofcomments: Article.comments, Topic: Article.topic };
    }

    // Cancel product details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update product details
    $scope.update = function () {
        if ($scope.Article.ArticleID != "" &&
       $scope.Article.title != "" && $scope.Article.datePub != "" && $scope.Article.topic != "") {
            $http({
                method: 'PUT',
                url: 'api/Article/PutArticle/' + $scope.Article.ArticleID,
                data: $scope.Article
            }).then(function successCallback(response) {
                $scope.articleData = response.data;
                $scope.clear();
                alert("Article Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete product details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            //Id or ArticlID
            url: 'api/Article/DeleteArticle/' + $scope.articleData[index].ArticleID,
        }).then(function successCallback(response) {
            $scope.articleData.splice(index, 1);
            alert("Book Deleted Successfully !!!");
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('ArticleService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('api/Article/GetAllArticles');
    }
    return fac;
});