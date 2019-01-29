// Brunch config
// 
// Derleme ve �al��t�rma i�lemlerini yapar.
//
// Detaylar i�in http://brunch.io
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