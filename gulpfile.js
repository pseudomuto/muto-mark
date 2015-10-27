"use strict";

require("babel/register");

var gulp = require("gulp");
var $    = require("gulp-load-plugins")();

var del            = require("del");
var ElectronServer = require("electron-connect").server;

var RUN_DIR  = ".run";
var SRC_DIR  = "app";

var runPath   = function(path) { return RUN_DIR + path; }
var srcPath   = function(path) { return SRC_DIR + path; }
var task      = function(file, options) { return require("./tasks/" + file)(gulp, $, options); }
var watchTask = function(file, watch, target) { return task(file, { watch: watch, target: target }); }

var Sources = {
  scripts:  "/**/*.js",
  styles:   "/style/**/*.{scss,css}",
  views:    "/views/**/*.html"
};

gulp.task("lint", task("lint", { paths: [srcPath(Sources.scripts)] }));
gulp.task("test", ["lint"], task("test", {
  tests: "test/**/*_test.js",
  scripts: srcPath(Sources.scripts),
  args: process.argv
}));

gulp.task("clean", task("clean", { delFn: del, targets: [RUN_DIR] }));
gulp.task("scss:watch", watchTask("scss-watch", srcPath(Sources.styles), runPath("/style")));
gulp.task("scripts:watch", watchTask("scripts-watch", srcPath(Sources.scripts), RUN_DIR));
gulp.task("views:watch", watchTask("copy-watch", srcPath(Sources.views), runPath("/views")));

gulp.task("default", ["scss:watch", "scripts:watch", "views:watch"], function() {
  var server = ElectronServer.create({ electron: require("electron-prebuilt") });
  server.start();

  var watches = Object.keys(Sources).map(function(key) { return runPath(Sources[key]); });
  gulp.watch(watches, server.reload);
  gulp.watch([runPath("/main.js")], server.restart);
});
