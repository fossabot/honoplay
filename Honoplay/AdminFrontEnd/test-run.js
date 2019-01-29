const fs = require('fs');
const walk = require('fs-walk');
const opn = require('opn');

const beginToken = "<!--%BEGIN%-->";
const endToken = "<!--%END%-->";
// �ablon dosyay� oku
let testFileContent = fs.readFileSync('test-run.html', { 'encoding': 'utf8' });

// ba�lang�� konumu
const startIndex = testFileContent.indexOf(beginToken) + beginToken.length;
// biti� konumu
const getEndIndex = () => testFileContent.indexOf(endToken);

// temizle 
testFileContent = testFileContent.slice(0, startIndex).concat(testFileContent.slice(getEndIndex(), testFileContent.length));

// test dosyalar�n� ekle
walk.walkSync('./test/', function (basedir, filename, stat) {
    testFileContent = testFileContent.slice(0, startIndex) + `    <script src="test/${filename}"></script>\r\n` + testFileContent.slice(startIndex, testFileContent.length);
});

// �ablonu kaydet
fs.writeFileSync("test-run.html", testFileContent);

// testi �al��t�r
opn(`file:///${__dirname}/test-run.html`, { app: ['chrome', '--new-window', '--incognito'] });

//chrome --headless --disable-gpu --dump-dom