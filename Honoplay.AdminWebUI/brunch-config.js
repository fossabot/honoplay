// Brunch config
// 
// Derleme ve çalýþtýrma iþlemlerini yapar.
//
// Detaylar için http://brunch.io
//

exports.files = {
    javascripts: {
        joinTo: {
            'vendor.js': /^(?!app)/,
            'app.js': /^app/
        }
    },
    stylesheets: { joinTo: 'app.css' }
};

exports.plugins = {
    babel: { presets: ["@babel/preset-env", "@babel/preset-react"] }
};