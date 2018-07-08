var webpack = require('webpack');
var path = require('path');
var pkg = require('./package.json');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ngAnnotatePlugin = require('ng-annotate-webpack-plugin');
var CleanUpPlugin = require('webpack-cleanup-plugin');
var ExtractTextPlugin = require("extract-text-webpack-plugin");


module.exports = {
    module: {
        loaders: [
            { test: /\.html$/, loader: 'html-loader' },
            {
                test: /\.js?$/,
                exclude: /(node_modules)/,
                loader: 'babel-loader',
                query: { presets: ['es2015'], cacheDirectory: true }
            },
            // { test: /\.css$/, loaders: [ 'style', 'css'] },
            { test: /\.scss$/, loaders: [ 'style', 'css', 'sass' ] },
            { test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "url?limit=10000" },
            { test: /\.(eot|svg|ttf|woff(2)?)(\?v=\d+\.\d+\.\d+)?/, loader: 'url' },
            { test: /\.(jpe?g|png|gif)$/i, loaders: [ 'file', 'image-webpack' ] }
        ]
    },
    context: __dirname,
    entry : {
        app: ['webpack/hot/dev-server', './src/index.js'],
        vendor: Object.keys(pkg.dependencies)
    },
    output: {
        path: path.join(__dirname, "dist"),
        filename: "app.min-[hash:6].js"
    },
    devServer: {
        port: 5000,
        historyApiFallback: true,
        inline: true
    },
    devtool: "source-map",
    plugins: [
        new CleanUpPlugin(),
        new ExtractTextPlugin("app.css"),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
            "window.jQuery": "jquery"
        }),
        new webpack.optimize.CommonsChunkPlugin('vendor', 'vendor.min-[hash:6].js'),
        new HtmlWebpackPlugin({
            template : 'src/index.ejs',
            title : 'Project Leo'
        })
        //,
        // new ngAnnotatePlugin({
        //     add : true
        // }),
        // new webpack.optimize.UglifyJsPlugin({
        //     compress: {
        //         warnings: true
        //     },
        //     mangle: false
        // })
    ]
}
