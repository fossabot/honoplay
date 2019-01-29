// Server.js
// 
// Visual Studio �zerinde �al���rken play d��mesinin uygulamay� �al��t�rmas�n� sa�lar.
//
// 

'use strict';
const util = require('util');
const exec = util.promisify(require('child_process').execSync);
const opn = require('opn');

console.log("Version. 1.0");

async function runServer() {

    opn('http://localhost:3333/', { app: ['chrome', '--remote-debugging-port=9222', '--new-window'] }).then(() => {
        console.log(`browser started`);
    });

    const { stdout, stderr } = await exec('yarn start', { stdio: 'inherit' });

    console.log('stdout:', stdout);
    console.log('stderr:', stderr);
}



runServer();



