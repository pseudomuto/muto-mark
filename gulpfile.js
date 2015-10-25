"use strict";

var gulp = require("gulp");
var $    = require("gulp-load-plugins")();

var Delete         = require("del");
var ElectronServer = require("electron-connect").server;

var RUN_DIR  = ".run";
var SRC_DIR  = "src";

var runPath = function(path) { return RUN_DIR + path; }
var srcPath = function(path) { return SRC_DIR + path; }

var Sources = {
  scripts:  "/**/*.js",
  styles:   "/assets/css/**/*.{scss,css}",
  views:    "/views/**/*.html"
};

gulp.task("run:sass", function(done) {
  gulp.src(srcPath(Sources.styles))
    .pipe($.watch(srcPath(Sources.styles), { verbose: true }))
    .pipe($.plumber())
    .pipe($.sass())
    .pipe(gulp.dest(runPath("/assets/css")));

  done();
});

gulp.task("run:views", function(done) {
  gulp.src(srcPath(Sources.views))
    .pipe($.watch(srcPath(Sources.views), { verbose: true }))
    .pipe(gulp.dest(runPath("/views")));

  done();
});

gulp.task("run:scripts", function(done) {
  gulp.src(srcPath(Sources.scripts))
    .pipe($.watch(srcPath(Sources.scripts), { verbose: true }))
    .pipe($.plumber())
    .pipe($.babel({ stage: 0 }))
    .pipe(gulp.dest(RUN_DIR));

  done();
});

gulp.task("run", ["run:sass", "run:scripts", "run:views"], function() {
  var server = ElectronServer.create({
    electron: require("electron-prebuilt")
  });

  server.start();

  var watches = Object.keys(Sources).map(function(key) {
    return runPath(Sources[key]);
  });

  gulp.watch(watches, server.reload);
  gulp.watch([runPath("/main.js")], server.restart);
});

gulp.task("clean", function(done) {
  Delete([RUN_DIR], function() {
    done();
  });
});
