module.exports = function(gulp, plugins, options) {
  var watch  = options.watch;
  var target = options.target;

  return function(done) {
    gulp.src(watch)
      .pipe(plugins.watch(watch, { verbose: true }))
      .pipe(plugins.plumber())
      .pipe(plugins.sass())
      .pipe(gulp.dest(target));

    done();
  };
};
