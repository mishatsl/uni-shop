


//$(document).ready(function () {
$(function () {
 //   "use strict"
    $('.upload-main-foto, .upload-foto').click(function (e) {
        if (e.target === $('a.delete-photo-a.icon.mini.vis.remove3.abs')[0]) {
            var Id = $(this).siblings('.id').val();
            $this = $(e.target);
            filePath = $this.parent('a').children('img').attr('src');
            var arr = filePath.split('/');
            if (arr.length > 0) {
                fileName = arr[arr.length - 1];
            }
            else {
                return;
            }
            $.ajax({
                type: "POST",
                url: '/admin/DeleteTemp/',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ "fileName": fileName }),
                success: function (result) {

                    $this.parent("a").children('img').css({ 'display': 'none' });
                    $this.parent("a").children('.lds-roller').css({ 'display': 'none' });
                    $this.parent("a").children('.photos-show-mini a i').css({ 'display': 'inline-block' });
                    $this.remove();
                    // $this.siblings("a").children('.lds-roller').css({ 'display': 'inline-block' });
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] === "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                }
            });
        }
        else {
            $(this).siblings('#mainImageLoading, .ImageLoading').click();
        }
        
    });




    $('.photos-show-mini .icon.remove3').click(function () {
        var Id = $(this).siblings('.id').val();
        var filePath = $(this).siblings('a').children('img').attr('src');
        var $this = $(this);
        var $br5 = $this.parent(".br5");
        var $State = $this.siblings('.State');
        var arr = filePath.split('/');
        var fileName = arr[arr.length - 1];
        if (arr.length > 0) {
            fileName = arr[length - 1];
        }
        else {
            return;
        }
        $.ajax({
            type: "POST",
            url: '/admin/DeleteTemp/',
            beforeSend: function (jqXHR, settings) {
               // $State.attr('value', 'delete');
                //    $this.siblings("a").children('.photos-show-mini a i').css({ 'display': 'none' });
                //    $this.siblings("a").children('.lds-roller').css({ 'display': 'inline-block' });
                //    $this.siblings("a").append('<a class="delete-photo-a icon mini vis remove3 abs" title="Удалить" href="#" rel="1">Delete</a>');
            },

            data: fileName,
            success: function (result) {
                $br5.removeClass("loaded");
                $br5.removeClass("loadeding");
                if ($State.attr('value') === 'saved' || $State.attr('value') === 'update')
                    $State.attr('value', 'delete');
                //$this.siblings('a').children('img').hide();
                //$this.siblings("a").children('.lds-roller').hide();
                //$this.siblings("a").children('.photos-show-mini a i').show();
                //$this.remove();

                // $this.siblings("a").children('.lds-roller').css({ 'display': 'inline-block' });
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] === "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    });


    $('#txtUploadFile, .ImageLoading').on('change', function (e) {
            var files = $(this)[0].files;
            var myID = $(this).siblings('.id').val() //uncomment this to make sure the ajax URL works
        var $this = $(this);
        var $br5 = $this.parent(".br5");
        var $State = $this.siblings('.State');
            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    //var data = new FormData();
                    //for (var x = 0; x < files.length; x++) {
                    //    data.append("file" + x, files[x]);
                    //}
                    // var data = optimizeImageBeforeUpload(files)
                    
                    var reader = new FileReader();
                    
                    //reader.onload = function (e) {
                    //    this.src = e.target.result;
                    //}
                   

                    reader.onload = function (evt) {
                        //if (evt.target.readyState == FileReader.DONE) {
                            var canvas = document.createElement('canvas');
                            //var ctx = canvas.getContext("2d");
                            //ctx.drawImage(img, 0, 0);
                            var img = new Image();
                            img.src = evt.target.result;
                            img.onload = function (e) {
                                var MAX_WIDTH = 600;
                                var MAX_HEIGHT = 600;

                                var width = img.width;
                                var height = img.height;

                                if (width > height) {
                                    if (width > MAX_WIDTH) {
                                        height *= MAX_WIDTH / width;
                                        width = MAX_WIDTH;
                                    }
                                } else {
                                    if (height > MAX_HEIGHT) {
                                        width *= MAX_HEIGHT / height;
                                        height = MAX_HEIGHT;
                                    }
                                }
                                //canvas.width = width;
                                //canvas.height = height;
                                var imgSize;
                                if (width > height)
                                    imgSize = width;
                                else
                                    imgSize = height;
                                canvas.width = imgSize;
                                canvas.height = imgSize;
                                var ctx = canvas.getContext("2d");
                                ctx.drawImage(img, (600 - imgSize) / 2, (600 - imgSize) / 2, width, height);
                                //ctx.drawImage(img, (600 - width) / 2, (600 - height) / 2, 0, 0, 600, 600);
                                var dataurl = canvas.toDataURL();
                                var photoIndex = $this.parents('li').attr('data-slot');
                                var tempFile = dataURLtoFile(dataurl, photoIndex + "_" + Date.now() + '.png');
                                var data = new FormData();

                                data.append("file", tempFile);
                                // var data = files[0];//dataURLtoFile(dataurl, myID+'.png');
                                //return dataurl;
                                $.ajax({
                                    type: "POST",
                                    url: '/admin/UploadTemp/' + myID,
                                    beforeSend: function (jqXHR, settings) {
                                        //$this.siblings("a").children('.photos-show-mini a i').hide();
                                        //$this.siblings("a").children('.lds-roller').show();
                                        //$this.siblings('a').children('img').hide();
                                        //$State.attr('value', 'add');
                                        $br5.removeClass("loaded");
                                        $br5.addClass("loading");
                                    },
                                    contentType: false,
                                    processData: false,
                                    //data: optimizeImageBeforeUpload($('.upload-main-foto, .upload-foto').siblings('#mainImageLoading, .ImageLoading')),
                                    data: data,
                                    success: function (result) {
                                        // $this.parents("li").html(result);
                                        var $img = $this.siblings('a').children('img');
                                        $img.attr('src', "/temp/" + result);
                                        if (width > height) {
                                            $img.css({ "width": "inherit" });
                                            $img.css({ "height": "auto" });
                                        }
                                        else {
                                            $img.css({ "width": "auto" });
                                            $img.css({ "height": "inherit" });
                                        }
                                        //$img.css({ 'display': 'inline-block' });
                                        //$this.siblings("a").children('.lds-roller').css({ 'display': 'none' });
                                        //$img.show();
                                        // $this.siblings("a").children('.lds-roller').css({ 'display': 'inline-block' });
                                        $br5.toggleClass("loading");
                                        $br5.toggleClass("loaded");
                                        if ($State.attr('value') === 'doNothing')
                                            $State.attr('value', 'add');
                                        else if ($State.attr('value') === 'saved')
                                            $State.attr('value', 'update');
                                        //else
                                        //    $State.attr('value', 'add');
                                    },
                                    error: function (xhr, status, p3, p4) {
                                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                                        if (xhr.responseText && xhr.responseText[0] === "{")
                                            err = JSON.parse(xhr.responseText).Message;
                                        console.log(err);
                                    }
                                });
                           // }
                        }
                    }

                    reader.readAsDataURL(files[0]);
                }

                else {
                    alert("This browser doesn't support HTML5 file uploads!");
                }
            }

           
        });
    });


$(window).on('reload', function (e) {

    var href = window.location.href;

    if (href.includes('product//product')) {
        var IsChangedForm = false;
        $('.form-group input, #txtUploadFile, ').each(function (input) {
            if (input.value == input.defaultValue) {
                alert("Please change field.");
                IsChangedForm = true;
            }
        });
        if (IsChangedForm) {
            var reload = confirm("There are not saved data. Are you sure you want reload this page!");
            if (reload) {
                e.preventDefault();
                window.location.href = href;
            }
            else {
                e.preventDefault();
                return;
            }
        }
    }
    });

function LineDeletedSuccess(ProductID)
{
    if(ProductID === 'undefined')
        return

    $('')
}

    function dataURLtoFile(dataurl, filename) {
        var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
        while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
        }
        return new File([u8arr], filename, { type: mime });
    }


//function optimizeImageBeforeUpload(files) {
//    // from an input element
//    //input = $input[0];
//    //var filesToUpload = input.files;
//    var file = files[0];
//    var img = document.createElement("img");
//    var reader = new FileReader();
//    reader.onload = function (e) {
//        img.src = e.target.result;
//    }


//    reader.readAsDataURL(file);

//    reader.onloadend = function () {
//        var canvas = document.createElement('canvas');
//        var ctx = canvas.getContext("2d");
//        ctx.drawImage(img, 0, 0);

//        var MAX_WIDTH = 600;
//        var MAX_HEIGHT = 600;

//        var width = img.width;
//        var height = img.height;

//        if (width > height) {
//            if (width > MAX_WIDTH) {
//                height *= MAX_WIDTH / width;
//                width = MAX_WIDTH;
//            }
//        } else {
//            if (height > MAX_HEIGHT) {
//                width *= MAX_HEIGHT / height;
//                height = MAX_HEIGHT;
//            }
//        }
//        canvas.width = width;
//        canvas.height = height;
//        var ctx = canvas.getContext("2d");
//        ctx.drawImage(img, 0, 0, width, height);
//        var dataurl = canvas.toDataURL();
//        return dataurl;
//    }
//}