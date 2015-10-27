var testSettings = {
  ui:        "tdd",
  reporter:  "dot",
  require:   ["./test/helper.js"]
};

var watchFiles = function(gulp, plugins, tests, scripts) {
  plugins.watch([tests, scripts], function() {
    runTests(gulp, plugins, tests);
  });

  runTests(gulp, plugins, tests);
}

var runTests = function(gulp, plugins, tests) {
  gulp.src(tests)
    .pipe(plugins.plumber())
    .pipe(plugins.mocha(testSettings));
}

module.exports = function(gulp, plugins, options) {
  var scripts = options.scripts;
  var tests   = options.tests;
  var watch   = options.args && options.args[options.args.length - 1] === "-w";

  return function(done) {
    watch ?
       watchFiles(gulp, plugins, tests, scripts) :
       runTests(gulp, plugins, tests);

    done();
  };
};
