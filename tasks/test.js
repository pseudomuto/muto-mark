var testSettings = {
  ui:        "tdd",
  reporter:  "dot",
  require:   ["./test/helper.js"]
};

module.exports = function(gulp, plugins) {
  return function() {
    return gulp.src("test/**/*_test.js")
      .pipe(plugins.mocha(testSettings));
  };
};
