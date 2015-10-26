module.exports = function(gulp, plugins, options) {
  var paths = options.paths;

  return function() {
    return gulp.src(paths)
      .pipe(plugins.eslint())
      .pipe(plugins.eslint.format())
      .pipe(plugins.eslint.failOnError());
  };
};
