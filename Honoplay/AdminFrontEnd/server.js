// Server.js
// 
// Visual Studio üzerinde çalışırken play düğmesinin uygulamayı çalıştırmasını sağlar.
//
// 

'use strict';
const util = require('util');
const exec = util.promisify(require('child_process').execSync);

async function runServer() {
    const { stdout, stderr } = await exec('yarn start', { stdio: 'inherit' });
    console.log('stdout:', stdout);
    console.log('stderr:', stderr);
}
runServer();
