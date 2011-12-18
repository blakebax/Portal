/// <reference path="jquery-1.5.1-vsdoc.js" />
// Namespace the page
var index_globals = {}; 
// Set default texts
index_globals.defaultTexts = ["e.g. Kaiser", "e.g. Sousa", "e.g. Kaiser@whoAmI.com", "e.g. 1234567"];

$(document).ready(function () {
    index_globals.PageLoad();
    index_globals.AjaxEvents();
    index_globals.RemoveButtons();
});

index_globals.PageLoad = function () {
    // Set the default text to prevent browser caching
    for (var i = 0; i < index_globals.defaultTexts.length; i++) { $(".addInput").eq(i).val(index_globals.defaultTexts[i]); }
    
    // Position the add table form over the table
    $("#addSubject").position({
        of: $("#subjectTable"),
        my: "left top",
        at: "left top",
        collision: "none none"
    });
    $("#addSubject").hide();

    // Add image swap
    $("#add_img").live("mouseover", function () {
        $(this).attr("src", "/Content/adddark.png");
    }).mouseout(function () {
        $(this).attr("src", "/Content/add.png");
    });

    // Show form button
    $("#add_img").live("click", function () {
        if ($("#addSubject").is(":hidden")) {
            // Formates the add subject widths
            $("#addSubject").css("height", $("#subjectTable").height());
            $("#addSubject").css("width", $("#subjectTable").width());
            $("#addSubject").fadeIn();
        } 
        else $("#addSubject").fadeOut();
    });

    // No outside links on this form :(
    $(".page a").live("click", function (e) {
        if(!$(this).hasClass("email"))
            e.preventDefault(); 
    });

    // Close form button
    $("#close_add").click(function (e) {
        $("#addSubject").fadeOut();
    });

    // Clears the default of the inputs, changes font color
    $(".addInput").each(function () {
        var defaultText = $(this).val();
        $(this).focus(function () {
            if ($(this).val() == defaultText) {
                $(this).val("");
                $(this).css("color", "#333333");
            }
        }).blur(function () {
            if ($(this).val() == "") {
                $(".addButton").fadeOut();
                $(this).siblings(".checkHolder").children(".check").fadeOut();
                $(this).val(defaultText);
                $(this).css("color", "#999999");
            }
     });

        // Validates the form inputs on keyup
        $(".addInput").not("#email_input").keyup(function () {
            if ($(this).val() == "") {
                $(this).siblings(".checkHolder").children(".check").fadeOut();
                $("#addButton").fadeOut();
            }
            if ($(this).val() != "" && $(this).val() !== defaultText) {
                $(this).siblings(".checkHolder").children(".check").fadeIn();

                // Allow input if all fields are filled
                if ($(".check:visible").length == 4) {
                    $("#addButton").fadeIn();
                }
            }
        });
    });
};

index_globals.AjaxEvents = function () {

    // Adds a subject to the table
    // Also does some animation on complete
    $("#addButton").live("click", function () {
        var firstLast = $("#firstname_input").val() + " " + $("#lastname_input").val();
        $.ajax({
            url: "/Home/AddToSubjectTable",
            data: ({
                FirstName: $("#firstname_input").val(),
                LastName: $("#lastname_input").val(),
                Email: $("#email_input").val(),
                Password: $("#pw_input").val(),
                SortBy: $("#sortByValue").val(),
                PageNumber: ($("#currentPageNumber").text() - 1)
            }),
            success: function (data) {
                $("#ajaxHtmlContainer").html("");
                $("#ajaxHtmlContainer").append(data);
                $("#addSubject").fadeOut("fast", function () {
                    $("#personSuccessIndicator span").text(firstLast + " Added");
                    $("#personSuccessIndicator").fadeIn("slow", function () {
                        $(this).fadeOut("slow");
                    });
                });
                $("#addButton").fadeOut()
                $(".check").hide();
                // reset defaults
                $("#firstname_input").val(index_globals.defaultTexts[0]);
                $("#lastname_input").val(index_globals.defaultTexts[1])
                $("#email_input").val(index_globals.defaultTexts[2]);
                $("#pw_input").val(index_globals.defaultTexts[3])
                $(".addInput").css("color", "#999999");
                index_globals.RemoveButtons();

            },
            error: function () {
                // Handle ajax errors here, log etc
            }
        });
    });

    // simple sorting
    $(".sort").live("click", function () {
        var newSort = $(this).attr("type");
        $.ajax({
            url: "/Home/UpdateSubjectTable",
            data: ({
                SortBy: newSort,
                PageNumber: (parseInt($("#currentPageNumber").text()) - 1)
            }),
            success: function (data) {
                $("#ajaxHtmlContainer").html("");
                $("#ajaxHtmlContainer").append(data);
                index_globals.RemoveButtons();
            }
        });
    });

    // simple page back
    $(".pager[type='back']").live("click", function () {
        var pageNumber = parseInt($("#currentPageNumber").text());
        if (pageNumber > 1) {
            $.ajax({
                url: "/Home/UpdateSubjectTable",
                data: ({
                    SortBy: $("#sortByValue").val(),
                    PageNumber: (pageNumber - 2)
                }),
                success: function (data) {
                    $("#ajaxHtmlContainer").html("");
                    $("#ajaxHtmlContainer").append(data);
                    index_globals.RemoveButtons();
                }
            });
        }
    });

    // simple page forward
    $(".pager[type='next']").live("click", function () {
        var pageNumber = parseInt($("#currentPageNumber").text());
        var totalPages = parseInt($("#totalPages").text());
        if (pageNumber < totalPages) {
            var sort = $("#sortByValue").val();
            $.ajax({
                url: "/Home/UpdateSubjectTable",
                data: ({
                    SortBy: sort,
                    PageNumber: pageNumber
                }),
                success: function (data) {
                    $("#ajaxHtmlContainer").html("");
                    $("#ajaxHtmlContainer").append(data);
                    index_globals.RemoveButtons();
                }
            });
        }
    });

    // Removes a row
    $(".removeActionBtn").live("click", function () {
        var $row = $("tr").not(".tableDark").eq($(".removeActionBtn").index(this));
        var firstLast = $row.children("td").eq(0).text() + $row.children("td").eq(1).text();
        var email = $row.children("td").eq(2).children("a").text();
        $.ajax({
            url: "/Home/RemoveSubject",
            data: ({
                Email: email,
                SortBy: $("#sortByValue").val(),
                PageNumber: ($("#currentPageNumber").text() - 1)
            }),
            success: function (data) {
                $("#ajaxHtmlContainer").html("");
                $("#ajaxHtmlContainer").append(data);
                index_globals.RemoveButtons();
                $("#personSuccessIndicator span").text(firstLast + " Deleted");
                $("#personSuccessIndicator").fadeIn("slow", function () {
                    $(this).fadeOut("slow");
                });
            },
            error: function () {
                // Handle ajax errors here, log etc
            }
        });
    });

    // I could do request throttling here if needed
    // Checks if the email already exists
    // success: "False" - show error, "True" - Allow submittal
    $("#email_input").keyup(function (e) {
        $this = $(this);
        $("#uniqueText").hide();
        if ($this.val() == "") {
            $this.siblings(".checkHolder").children(".check").fadeOut();
            $("#addButton").fadeOut();
        }
        if ($this.val() != "" && $this.val() != index_globals.defaultTexts[2]) {
            $.ajax({
                url: "/Home/UniqueCheck",
                data: ({
                    Email: $this.val()
                }),
                success: function (data) {
                    if (data == "True") {
                        $this.siblings(".checkHolder").children(".check").fadeIn();

                        // Allow input if all fields are filled
                        if ($(".check:visible").length == 4) {
                            $("#addButton").fadeIn();
                        }
                    }
                    else {
                        $("#addButton").fadeOut();
                        $this.siblings(".checkHolder").children(".check").fadeOut("fast", function () {
                            $("#uniqueText").show();
                        });

                    }
                }
            });
        }
    });
};

// Adds a "(delete)" button beside each column
index_globals.RemoveButtons = function () {
    var i = 0;
    $(".removeBtn").remove();
    $("#subjectTable tr").not(".tableDark ").each(function () {
        $("#buttonTemplate").tmpl({}).appendTo(".page");
        $(".removeBtn").eq(i).position({
            of: $(this).children("td:last"),
            my: "left top",
            at: "right top",
            collision: "none none",
            offset: "4 4"
        });
        i++;
    });
};