var path = require('path');

var appRoot = 'src/';
var outputRoot = 'dist/';

module.exports = {
    root: appRoot,
    source: appRoot + '**/*.ts',
    dtssource: 'dts/**/*.d.ts',
    html: appRoot + '**/*.html',
    output: outputRoot,
    doc: './doc',
    e2eSpecsSrc: 'test/e2e/src/*.ts',
    e2eSpecsDist: 'test/e2e/dist/',
    sassDir: 'scss/'

};
