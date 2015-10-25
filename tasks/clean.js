module.exports = function(gulp, plugins, options) {
  var targets = options.targets;
  var delFn   = options.delFn;

  return function(done) {
    delFn(targets, function() {
      done();
    });
  };
};
