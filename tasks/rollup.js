const babel = require('rollup-plugin-babel')
const rollup = require('rollup').rollup

const nodeBuiltInModules = [
  'assert', 'buffer', 'child_process', 'cluster', 'console', 'constants', 'crypto', 'dgram', 'dns', 'domain', 'events',
  'fs', 'http', 'https', 'module', 'net', 'os', 'path', 'process', 'punycode', 'querystring', 'readline', 'repl',
  'stream', 'string_decoder', 'timers', 'tls', 'tty', 'url', 'util', 'v8', 'vm', 'zlib'
]

const electronBuiltInModules = ['electron']

const npmModulesUsedInApp = () => {
  const appManifest = require('../app/package.json')
  return Object.keys(appManifest.dependencies)
}

const generateExternalModulesList = () => {
  return [].concat(nodeBuiltInModules, electronBuiltInModules, npmModulesUsedInApp())
}

const cache = {}

module.exports = (src, dest, options) => {
  options = options || {}
  options.rollupPlugins = options.rollupPlugins || [babel({ exclude: 'node_modules/**' })]

  return rollup({
    cache: cache[src],
    entry: src,
    external: generateExternalModulesList(),
    plugins: options.rollupPlugins
  }).then(bundle => {
    cache[src] = bundle
    return bundle.write({ format: 'cjs', dest: dest })
  })
}
