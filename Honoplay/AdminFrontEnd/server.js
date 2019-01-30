// Server.js
// 
// Visual Studio üzerinde çalışırken play düğmesinin uygulamayı çalıştırmasını sağlar.
//
// 

'use strict';
const util = require('util');
const exec = util.promisify(require('child_process').execSync);
const opn = require('opn');

console.log("Version. 1.0");
(async function () {
    opn('http://localhost:3000/', { app: ['chrome', '--disable-plugins', '--remote-debugging-port=9222', '--new-window', '--disable-translate', '--virtual-time-budget=10000'] }).then(() => {
        console.log(`browser started`);
    });
    await exec('yarn start', { stdio: 'inherit' });
})();
