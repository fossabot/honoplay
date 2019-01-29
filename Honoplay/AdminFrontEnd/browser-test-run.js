const fs = require('fs');
const walk = require('fs-walk');
const opn = require('opn');
const util = require('util');
const exec = util.promisify(require('child_process').execSync);
const beginToken = "<!--%BEGIN%-->";
const endToken = "<!--%END%-->";
// þablon dosyayý oku
let testFileContent = fs.readFileSync('browser-test-run.html', { 'encoding': 'utf8' });

// baþlangýç konumu
const startIndex = testFileContent.indexOf(beginToken) + beginToken.length;
// bitiþ konumu
const getEndIndex = () => testFileContent.indexOf(endToken);

// temizle 
testFileContent = testFileContent.slice(0, startIndex).concat(testFileContent.slice(getEndIndex(), testFileContent.length));

// test dosyalarýný ekle
walk.walkSync('./browser-test/', function (basedir, filename, stat) {

    exec(`yarn babel ${basedir}${filename} --out-file  ${basedir}${filename}-compiled.js`);
    testFileContent = testFileContent.slice(0, startIndex) + `    <script src="${basedir}${filename}-compiled.js"></script>\r\n` + testFileContent.slice(startIndex, testFileContent.length);
});

// þablonu kaydet
fs.writeFileSync("test-run.html", testFileContent);

// testi çalýþtýr
opn(`file:///${__dirname}/test-run.html`, { app: ['chrome', '--new-window', '--incognito', '--disable-translate' ,'--disable-plugins'] });

//chrome --headless --disable-gpu --dump-dom