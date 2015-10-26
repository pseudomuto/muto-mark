var testSettings = {
  ui:        "tdd",
  reporter:  "dot",
  require:   ["./test/helper.js"]
};

module.exports = function(gulp, plugins, options) {
  var glob = options.glob;

  return function() {
    return gulp.src(glob)
      .pipe(plugins.mocha(testSettings));
  };
};
