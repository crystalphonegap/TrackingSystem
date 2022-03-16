/* Meme */

var memes = [
	'Type <b>Hi</b>'
	
];

checkCookie();

var random = document.querySelector('#random');

random.innerHTML = memes[Math.floor(Math.random() * memes.length)];

/* Time */

var deviceTime = document.querySelector('.status-bar .time');
var messageTime = document.querySelectorAll('.message .time');

deviceTime.innerHTML = moment().format('h:mm');

setInterval(function () {
	deviceTime.innerHTML = moment().format('h:mm');
}, 1000);

for (var i = 0; i < messageTime.length; i++) {
	messageTime[i].innerHTML = moment().format('h:mm A');
}

/* Message */

var form = document.querySelector('.conversation-compose');
var conversation = document.querySelector('.conversation-container');

conversation.scrollTop = conversation.scrollHeight;

form.addEventListener('submit', newMessage);

function newMessage(e) {
	var input = e.target.input;

	if (input.value) {
		var message = buildMessage(input.value);
		conversation.appendChild(message);
		animateMessage(message);

		//var IP = CallDialogue(input.value);
		
		//alert(IP);
		//if (getCookie("sessionid"))
		//{
		//	var SendByBot = CallDialogue(input.value);
		//	var Botr = BotAnswer(SendByBot);
		//	conversation.appendChild(Botr);
		//	animateMessage(Botr);
		//}
		
	}

	var Cookie = getCookie("sessionid");
	if (Cookie) {
		fetchUserData(input.value, Cookie);	
	}
	input.value = '';
	conversation.scrollTop = conversation.scrollHeight;
	//if (getCookie("sessionid")) {
	

	//}
	e.preventDefault();
	
}

function buildMessage(text) {
	var element = document.createElement('div');


	element.classList.add('message', 'sent');
	element.innerHTML = whatsappStyles(text, '*', '<b>', '</b>') +
		'<span class="metadata">' +
		'<span class="time">' + moment().format('h:mm A') + '</span>' +
		'<span class="tick tick-animation">' +
		'<svg xmlns="http://www.w3.org/2000/svg" width="16" height="15" id="msg-dblcheck" x="2047" y="2061"><path d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.88a.32.32 0 0 1-.484.032l-.358-.325a.32.32 0 0 0-.484.032l-.378.48a.418.418 0 0 0 .036.54l1.32 1.267a.32.32 0 0 0 .484-.034l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.88a.32.32 0 0 1-.484.032L1.892 7.77a.366.366 0 0 0-.516.005l-.423.433a.364.364 0 0 0 .006.514l3.255 3.185a.32.32 0 0 0 .484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z" fill="#92a58c"/></svg>' +
		'<svg xmlns="http://www.w3.org/2000/svg" width="16" height="15" id="msg-dblcheck-ack" x="2063" y="2076"><path d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.88a.32.32 0 0 1-.484.032l-.358-.325a.32.32 0 0 0-.484.032l-.378.48a.418.418 0 0 0 .036.54l1.32 1.267a.32.32 0 0 0 .484-.034l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.88a.32.32 0 0 1-.484.032L1.892 7.77a.366.366 0 0 0-.516.005l-.423.433a.364.364 0 0 0 .006.514l3.255 3.185a.32.32 0 0 0 .484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z" fill="#4fc3f7"/></svg>' +
		'</span>' +
		'</span>';

	return element;
}

function TextHtmlFormatting(text) {
	var newText = "";
	var RexpUrl ="(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
	var RexNewLine = "/\n\r?/g";

	//newText = text.replace(RexpUrl, "<a href='$1' target='_blank'>$3</a>");
	newText = text.replace(/((http|https|ftp):\/\/[\w?=&.\/-;#~%-]+(?![\w\s?&.\/;#~%"=-]*>))/g, '<a href="$1">$1</a>').replace(/\n\r?/g, '<br />');
		//.replace(RexNewLine, '<br/>');

	return newText;
}


function BotAnswer(text) {
	var element = document.createElement('div');
	element.classList.add('message', 'received');

	//element.innerHTML = whatsappStyles(text.replace(/\n\r?/g, '<br />'), '*', '<b>', '</b>') +
	element.innerHTML = TextHtmlFormatting(whatsappStyles(text, '*', '<b>', '</b>')) +
		'<span class="metadata">' +
		'<span class="time">' + moment().format('h:mm A') + '</span>' +
		'<span class="tick tick-animation">' +
		'<svg xmlns="http://www.w3.org/2000/svg" width="16" height="15" id="msg-dblcheck" x="2047" y="2061"><path d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.88a.32.32 0 0 1-.484.032l-.358-.325a.32.32 0 0 0-.484.032l-.378.48a.418.418 0 0 0 .036.54l1.32 1.267a.32.32 0 0 0 .484-.034l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.88a.32.32 0 0 1-.484.032L1.892 7.77a.366.366 0 0 0-.516.005l-.423.433a.364.364 0 0 0 .006.514l3.255 3.185a.32.32 0 0 0 .484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z" fill="#92a58c"/></svg>' +
		'<svg xmlns="http://www.w3.org/2000/svg" width="16" height="15" id="msg-dblcheck-ack" x="2063" y="2076"><path d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.88a.32.32 0 0 1-.484.032l-.358-.325a.32.32 0 0 0-.484.032l-.378.48a.418.418 0 0 0 .036.54l1.32 1.267a.32.32 0 0 0 .484-.034l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.88a.32.32 0 0 1-.484.032L1.892 7.77a.366.366 0 0 0-.516.005l-.423.433a.364.364 0 0 0 .006.514l3.255 3.185a.32.32 0 0 0 .484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z" fill="#4fc3f7"/></svg>' +
		'</span>' +
		'</span>';
	return element;
}

function animateMessage(message) {
	setTimeout(function () {
		var tick = message.querySelector('.tick');
		tick.classList.remove('tick-animation');
	}, 500);
}






function fetchUserData(text,sessionid) {
	var name = "";
	var api = "/api/DialogflowApi/WABADemo/" + sessionid + "/" + text+"";
	fetch(api)
		.then(response => response.json())
		.then(function (commits) {
			name = commits.fulfillmentText
			console.log(name);
			if (name) {

				var message = BotAnswer(name);
				conversation.appendChild(message);
				animateMessage(message);
				conversation.scrollTop = conversation.scrollHeight;
			}

		});

	
	
}














function is_aplhanumeric(c) {
	var x = c.charCodeAt();
	return ((x >= 65 && x <= 90) || (x >= 97 && x <= 122) || (x >= 48 && x <= 57)) ? true : false;
}

function whatsappStyles(format, wildcard, opTag, clTag) {
	var indices = [];
	for (var i = 0; i < format.length; i++) {
		if (format[i] === wildcard) {
			if (indices.length % 2)
				(format[i - 1] == " ") ? null : ((typeof (format[i + 1]) == "undefined") ? indices.push(i) : (is_aplhanumeric(format[i + 1]) ? null : indices.push(i)));
			else
				(typeof (format[i + 1]) == "undefined") ? null : ((format[i + 1] == " ") ? null : (typeof (format[i - 1]) == "undefined") ? indices.push(i) : ((is_aplhanumeric(format[i - 1])) ? null : indices.push(i)));
		}
		else {
			(format[i].charCodeAt() == 10 && indices.length % 2) ? indices.pop() : null;
		}
	}
	(indices.length % 2) ? indices.pop() : null;
	var e = 0;
	indices.forEach(function (v, i) {
		var t = (i % 2) ? clTag : opTag;
		v += e;
		format = format.substr(0, v) + t + format.substr(v + 1);
		e += (t.length - 1);
	});
	return format;
}


















function setCookie(cname, cvalue, exdays) {
	var d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	var expires = "expires=" + d.toUTCString();
	document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function createCookie(name, value, days) {
	var expires;
	if (days) {
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		expires = "; expires=" + date.toUTCString();
	} else {
		expires = "";
	}
	document.cookie = escape(name) + "=" + escape(value) + expires + "; path=/";

}


function setCookie(sessionid, sessionidvalue, exdays) {
	var d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	var expires = "expires=" + d.toUTCString();
	document.cookie = sessionid + "=" + sessionidvalue + ";" + expires + ";path=/";
}

function getCookie(sessionid) {
	var name = sessionid + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}

function checkCookie() {
	var user = getCookie("sessionid");
	if (user != "") {
	} else {
		user = prompt("Please enter your mobile number:", "");
		if (user != "" && user != null) {
			setCookie("sessionid", user, 365);
		}
	}
}





