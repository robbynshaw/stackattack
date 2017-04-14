const path = require('path');
const webpack = require('webpack');
const wpNotify = require('webpack-notifier');

module.exports = {
    entry: {
        babel: "babel-polyfill",
        app: "./Scripts/src2/app.js"
    },
    output: {
        path: path.join(__dirname, '/Scripts/build'),
        filename: "[name].bundle.js"
    },
    devtool: 'source-map',
    plugins: [
        new wpNotify()
    ],
    module: {
        loaders: [
            {
                test: /\.js$/,
                include: path.join(__dirname, 'Scripts'),
                exclude: path.join(__dirname, 'Scripts', 'build'),
                loader: 'babel-loader',
                query: {
                    presets: ["es2015"]
                }
            },
            {
                test: /\.handlebars$/,
                loader: "handlebars-loader"
            }
        ]
    }
};