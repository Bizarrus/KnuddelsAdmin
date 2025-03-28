console.log(process.argv);

const WebSocket		= require('ws');
const socket		= new WebSocket("ws://127.0.0.1:" + process.argv[2]);

socket.on('open', () => socket.send(JSON.stringify({ uuid: process.argv[3], event: process.argv[4] })));
socket.on('close', process.exit);
socket.on('message', e => {
	console.log('message', e.data);
});

// To ensure compatibility with Elgato,
// we use the same function names as theirs, 
// so your developed plugin can be compatible with Elgato software.
function connectElgatoStreamDeckSocket(
    inPort, 
    inPluginUUID, 
    inRegisterEvent, 
    inInfo
) {
	console.log('connectElgatoStreamDeckSocket', arguments);
}