var question = [];
var correctAnswer;
var answer;
var index = 0;
var score = 0;

var uid = getCookie("uid");
var user_score;

var count_dialog = -1;

const ctx = $("#myChart");

$(document).ready(function () {

  //Cứ mỗi 5s thì sẽ cập nhật cái bảng xếp hạng
  setInterval(setLeaderBoard(), 5000);

  if (uid != null) {
    //get question
    $.ajax({
      headers: {
        "Access-Control-Allow-Origin": "*",
      },
      type: "GET",
      contentType: "application/json",
      Credential: "include",
      url: "https://localhost:7099/api/Questions",
      xhrFields: {
        withCredentials: true,
      },
      async: false,
      error: function () {
        console.log("Error");
      },
      success: function (data) {
        question = data;
      },
    });

    //lần đầu chơi thì sẽ lưu điểm với giá trị là 0
    //Sẽ update điểm nếu như người chơi kết thúc
    getScoreByUserId();
    if (user_score == null) {
      saveScore();
    }
    else { score = user_score.score; console.log(user_score.score); }


    displayConversation(true);
    setScore(score);
    setActiveQuestion(12 - index);
    setQuestion(index);
    setAnswerQuestion(index);
    $("#bar-percent-ask-audience").hide();

    console.log(correctAnswer);
  }
});

//set question
function setQuestion(count) {
  $("#question").empty().text(question[count].question_content);
}
//Set Answer Question
function setAnswerQuestion(question_number) {
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "GET",
    contentType: "application/json",
    Credential: "include",
    url:
      "https://localhost:7099/api/Answers/" +
      question[question_number].question_id,
    xhrFields: {
      withCredentials: true,
    },
    async: false,
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      for (var i = 0; i < data.length; i++) {
        if (data[i].isCorrect) {
          correctAnswer = data[i].answer_content;
        }
      }

      answer = data;

      $("#answer_a")
        .empty()
        .text(data[0].answer_id + ". " + data[0].answer_content);
      $("#answer_b")
        .empty()
        .text(data[1].answer_id + ". " + data[1].answer_content);
      $("#answer_c")
        .empty()
        .text(data[2].answer_id + ". " + data[2].answer_content);
      $("#answer_d")
        .empty()
        .text(data[3].answer_id + ". " + data[3].answer_content);
    },
  });
}

//Set Scores
function setScore(score) {
  $("#score")
    .empty()
    .text("Score: " + score);
}

//Set active question number
function setActiveQuestion(count) {
  $(".active-question-number").removeClass("active-question-number");

  console.log(count);
  $(".price-list .price:nth-child(" + count + ")").addClass(
    "active-question-number"
  );
}

//check if answer correct
$(".choosing-answer").on("click", function (e) {
  if ($(this).text().substring(3) == correctAnswer && correctAnswer != null) {
    correctAnswer = null;
    index++;
    score += 10000;
    setScore(score);


    //Nếu như câu hỏi hiện mà >= số lượng câu hỏi thì sẽ kết thúc game và chiến thắng
    if (index >= question.length) {
      displayAlertGameResult(true);
      location.reload();
    } else {
      setActiveQuestion(12 - index);
      setQuestion(index);
      setAnswerQuestion(index);
      resetAnswer();
    }
    $("#bar-percent-ask-audience").hide();

  }
  else {
    displayAlertGameResult(false);
  }
});

//display win game or lose game notification
function displayAlertGameResult(isWin) {
  updateScoreByUserId();
  if (isWin) {
    alert("Win!");
  }
  else {
    alert("Win!");
  }

}


// ----- Score ------

//lọc ra 10 người có số điểm cao nhất theo thứ tự từ list get đc từ api getScores
function setLeaderBoard()
{
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "GET",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Scores/GetScores",
    xhrFields: {
      withCredentials: true,
    },
    async: false,
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      console.log(data);
      $('#leader-board').empty();
      for(var i = 0; i < data.length; i++) {
        $('#leader-board').append('<li class="text-white fs-3">'+ data[i].score +'</li>');
      }
    },
  });
}

function saveScore() {
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "POST",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Scores/PostScore",
    xhrFields: {
      withCredentials: true,
    },
    async: false,
    data: JSON.stringify({
      user_id: uid,
      score: score,
    }),
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      console.log("Success Save Score");
    },
  });
}

function getScoreByUserId() {
  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "GET",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Scores/GetScoreByUserId/" + uid,
    xhrFields: {
      withCredentials: true,
    },
    async: false,
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      user_score = data;
    },
  });
}

function updateScoreByUserId() {

  //Get lại cái dữ liệu score để lấy dữ liệu
  getScoreByUserId();

  $.ajax({
    headers: {
      "Access-Control-Allow-Origin": "*",
    },
    type: "PUT",
    contentType: "application/json",
    Credential: "include",
    url: "https://localhost:7099/api/Scores/UpdateScore/" + uid,
    xhrFields: {
      withCredentials: true,
    },
    async: false,
    data: JSON.stringify({
      score_id: user_score.score_id,
      user_id: uid,
      score: score,
    }),
    error: function () {
      console.log("Error");
    },
    success: function (data) {
      console.log("Success Updates Score");
    },
  });
}

//----- helper ------

// Phone a Friend helper function
$("#phone-helpder").on("click", function () {
  displayConversation(false);
  conversationDialog(true);
  setDialog(true);
  $("#phone-helpder").attr("disabled", true);
});

$("#player-dialog").on("click", function () {
  conversationDialog(false);
  setDialog(false);
});

$("#friend-dialog").on("click", function () {
  conversationDialog(true);
  setDialog(true);
});

//Conversation show and hide
function displayConversation(isHide) {
  if (isHide) {
    $("#conversation").hide();
  } else {
    $("#conversation").show();
  }
}

function conversationDialog(isHide) {
  if (isHide) {
    $("#player-dialog").show();
    $("#friend-dialog").hide();
  } else {
    $("#player-dialog").hide();
    $("#friend-dialog").show();
  }
}

function setDialog(check) {
  count_dialog++;

  var list_conversation = [
    "Alo..., Là mình đây",
    "Là cậu à lâu lắm rồi mới gặp",
    "Bây giờ mình đang tham gia ai là triệu phú. Hiện mình đang ở câu hỏi số " +
    index +
    1 +
    " và cần sự trợ giúp của cậu.",
    "Ok",
    "Câu hỏi là " +
    question[index].question_content +
    "\n " +
    answer[0].answer_id +
    ". " +
    answer[0].answer_content +
    "\n " +
    answer[1].answer_id +
    ". " +
    answer[1].answer_content +
    "\n " +
    answer[2].answer_id +
    ". " +
    answer[2].answer_content +
    "\n " +
    answer[3].answer_id +
    ". " +
    answer[3].answer_content,
    "Ok. Theo mình đáp án đúng là " + correctAnswer,
    "Cảm ơn cậu",
  ];

  if (count_dialog < list_conversation.length) {
    if (check) {
      $("#player-dialog-conversation")
        .empty()
        .text(list_conversation[count_dialog]);
    } else {
      $("#friend-dialog-conversation")
        .empty()
        .text(list_conversation[count_dialog]);
    }
  } else {
    displayConversation(true);
  }
}

// Fifty fifty function
$("#fifty-fifty-helper").on("click", function () {
  var correct_num = 0;
  $("#fifty-fifty-helper").attr("disabled", true);

  for (var i = 0; i < answer.length; i++) {
    if (answer[i].isCorrect) {
      correct_num = i;
    }
  }

  var first = (correct_num + 1) % 4;
  var second = (first + 1) % 4;

  if (first == 0 || second == 0) {
    $("#answer_a")
      .attr("disabled", true)
      .addClass("text-danger")
      .removeClass("text-white");
  }
  if (first == 1 || second == 1) {
    $("#answer_b")
      .attr("disabled", true)
      .addClass("text-danger")
      .removeClass("text-white");
  }
  if (first == 2 || second == 2) {
    $("#answer_c")
      .attr("disabled", true)
      .addClass("text-danger")
      .removeClass("text-white");
  }
  if (first == 3 || second == 3) {
    $("#answer_d")
      .attr("disabled", true)
      .addClass("text-danger")
      .removeClass("text-white");
  }
});

function resetAnswer() {
  $("#answer_a")
    .attr("disabled", false)
    .addClass("text-white")
    .removeClass("text-danger");
  $("#answer_b")
    .attr("disabled", false)
    .addClass("text-white")
    .removeClass("text-danger");
  $("#answer_c")
    .attr("disabled", false)
    .addClass("text-white")
    .removeClass("text-danger");
  $("#answer_d")
    .attr("disabled", false)
    .addClass("text-white")
    .removeClass("text-danger");
}

//Ask audience function
$("#audience-helper").on("click", function () {
  $("#audience-helper").attr("disabled", true);

  var rd = Math.floor(Math.random() * (4 - 1) + 1); //Random ra ti le co the sai or dung
  var percent = [];

  switch (rd) {
    case 1:
      percent = [76, 4, 8, 12];
      break;

    case 2:
      percent = [7, 40, 8, 45];
      break;

    case 3:
      percent = [3, 8, 82, 7];
      break;

    case 4:
      percent = [2, 90, 5, 3];
      break;
  }

  $("#bar-percent-ask-audience").show();
  setBarPercentValue(percent);

  setTimeout(() => {
    $("#bar-percent-ask-audience").hide();
  }, 10000);
});
function setBarPercentValue(data) {
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: ["A", "B", "C", "D"],
      datasets: [
        {
          label: "% of Votes",
          data: data,
          borderColor: ["rgb(255, 99, 132)", "rgb(153, 102, 255)"],
          backgroundColor: [
            "rgba(54, 162, 235, 0.2)",
            "rgba(255, 159, 64, 0.2)",
            "rgba(201, 203, 207, 0.2)",
            "rgba(75, 192, 192, 0.2)",
          ],
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
        },
      },
    },
  });
}
