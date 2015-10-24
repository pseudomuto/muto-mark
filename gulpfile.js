"use strict";

var gulp = require("gulp");
var $    = require("gulp-load-plugins")();

var Delete         = require("del");
var ElectronServer = require("electron-connect").server;

var RUN_DIR  = ".run";
var SRC_DIR  = "src";

gulp.task("run:scripts", function(done) {
  gulp.src(SRC_DIR + "/**/*.js")
    .pipe($.watch(SRC_DIR + "/**/*.js", { verbose: true }))
    .pipe($.plumber())
    .pipe($.babel({ stage: 0 }))
    .pipe(gulp.dest(RUN_DIR));

  done();
});

gulp.task("run", ["run:scripts"], function() {
  var server = ElectronServer.create();
  server.start();

  gulp.watch([RUN_DIR +"/main.js"], server.restart);
  gulp.watch([RUN_DIR +"/**/*.js"], server.reload);
});

gulp.task("clean", function(done) {
  Delete([RUN_DIR], function() {
    done();
  });
});
