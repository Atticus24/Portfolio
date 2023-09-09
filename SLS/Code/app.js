var video = document.querySelector('video');
var recordButton = document.querySelector('button');

recordButton.onclick = function() {
  navigator.mediaDevices.getUserMedia({ video: true })
    .then(function(stream) {
      video.srcObject = stream;
      video.play();
      var mediaRecorder = new MediaRecorder(stream);
      mediaRecorder.ondataavailable = function(e) {
        // Do something with the captured frame
      };
      mediaRecorder.start();
    })
    .catch(function(err) {
      console.error('Error getting user media:', err);
    });
};