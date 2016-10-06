/// <reference path="node_modules/jquery/dist/jquery.min.js" />
module.exports = {
    entry: "./Scripts/src/candidates.js",
    output: {
        path: __dirname,
        filename: "/scripts/bundle.js"
    },
    resolve: {
        alias: {
            jquery: "jquery/dist/jquery.min.js"
        }
    }
};