var uid = getCookie("uid");
let user = null;

if (uid != null) {
  user = GetUserById(uid).then(function (result) {
    $(".display-username")
      .empty()
      .append("Hello, " + result.username);
    $("#user-btn").show();
    $("#login-btn").hide();
  });
} else {
  $("#user-btn").hide();
  $("#login-btn").show();
}

//Authentication function for login

// --- login ---
$("#login").on("click", function () {
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    contentType: "application/json",
    Credential: "include",
    type: "POST",
    url: "https://localhost:7099/api/Users/Login",
    data: JSON.stringify({
      username: $("#username").val(),
      password: $("#password").val(),
    }),
    xhrFields: {
      withCredentials: true,
    },
    error: function () {
      console.log("Error");
      $(".alert")
        .empty()
        .append("Login Fail!")
        .removeClass()
        .addClass("text-danger");
    },
    success: function (data) {
      $(".alert")
        .empty()
        .append("Login Success!")
        .removeClass()
        .addClass("text-success");

      console.log(data);

      $(".display-username")
        .empty()
        .append("Hello, " + data.username);
      $("#user-btn").show();
      $("#login-btn").hide();
    },
  });
});

// --- Register ---
$("#register").click(function () {
  if ($("#password-register").val() == $("#confirm-password").val()) {
    $.ajax({
      headers: {
        "Access-Control-Allow-Origin": "*",
      },
      contentType: "application/json",
      Credential: "include",
      type: "POST",
      url: "https://localhost:7099/api/Users/Register",
      data: JSON.stringify({
        name: $("#name").val(),
        username: $("#username-register").val(),
        password: $("#password-register").val(),
        role: "Player",
      }),
      xhrFields: {
        withCredentials: true,
      },
      error: function () {
        console.log("Error");
        $(".alert")
          .empty()
          .append("Register Fail!")
          .removeClass()
          .addClass("text-danger");
      },
      success: function (data) {
        $(".alert")
          .empty()
          .append("Register Success! Please login")
          .removeClass()
          .addClass("text-success");
      },
    });
  } else {
    $(".alert")
      .empty()
      .append("Register Fail!")
      .removeClass()
      .addClass("text-danger");
  }
});

//--- Display user
async function GetUserById(uid) {
  const obj = await $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "GET",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Users/GetUserById/" + uid,
    xhrFields: {
      withCredentials: true,
    },
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      return data;
    },
  });
  return obj;
}

//--- Update User ---
$("#update-user-btn").click(async function () {
  let id = getCookie("uid");
  var u = await GetUserById(uid);

  $("#notice-user-change").empty();

  $("#user-id").val(u.user_id);
  $("#role").val(u.role);
  $("#name").val(u.name);
  $("#email").val(u.email);
  $("#book-id").val(u.book_id);
});

$("#btn-update-save").click(function () {
  if ($("#passowrd").val() === $("#confirm-password").val()) {
    $.ajax({
      headers: {
        "Access-Control-Allow-Origin": "*",
      },
      contentType: "application/json",
      type: "PUT",
      Credential: "include",

      url: "https://localhost:7099/api/Users/UpdateUser/" + uid,
      xhrFields: {
        withCredentials: true,
      },
      data: JSON.stringify({
        user_id: $("#user-id").val(),
        name: $("#name").val(),
        email: $("#email").val(),
        book_id: $("#book-id").val(),
        password: $("#password").val(),
        role: $("#role").val(),
      }),
      error: function () {
        console.log("Error");
        $("#notice-user-change")
          .empty()
          .append("Update Fail!")
          .removeClass()
          .addClass("text-danger");
        setTimeout(() => $("#notice-user-change").empty(), 5000);
      },
      success: function (data) {
        $("#notice-user-change")
          .empty()
          .append("Update Success!")
          .removeClass()
          .addClass("text-success");
        $("#display-username").empty().append(user.userName);
        setTimeout(() => $("#notice-user-change").empty(), 5000);
      },
    });
  } else {
    $("#notice-user-change")
      .empty()
      .append("Update Fail!")
      .removeClass()
      .addClass("text-danger");
  }
});

//--- Log out ---
$("#logout-btn").click(function () {
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "POST",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Users/Logout",
    xhrFields: {
      withCredentials: true,
    },
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      location.reload();
    },
  });
});

function getCookie(name) {
  var nameEQ = name + "=";
  var ca = document.cookie.split(";");
  for (var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == " ") c = c.substring(1, c.length);
    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
  }
  return null;
}
