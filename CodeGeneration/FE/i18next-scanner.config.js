const fs = require("fs");
const chalk = require("chalk");

const extensions = [".js", ".jsx", "ts", "tsx"];

const options = {
  debug: true,

  func: {
    list: ["translate", "__"],
    extensions,
  },

  trans: {
    component: "Trans",
    i18nKey: "i18nKey",
    defaultsKey: "defaults",
    extensions,
    acorn: {
      ecmaVersion: 10,
      sourceType: "module",
    },
  },
  lngs: ["en", "vi"],
  resource: {
    loadPath: "{{lng}}.json",
    savePath: "{{lng}}.json",
    jsonIndent: 2,
    lineEnding: "\n",
  },
  keySeparator: ".",
  interpolation: {
    prefix: "{{",
    suffix: "}}",
  },
};

module.exports = {

  input: [
    "src/**/*.{js,jsx,ts,tsx}",
    "!src/**/*.spec.{js,jsx,ts,tsx}",
    "!src/**/*.test.{js,jsx,ts,tsx}",
    "!src/i18n/**",
    "!**/node_modules/**",
  ],

  output: "./public/assets/i18n/",

  options,

  transform: function customTransform(file, enc, done) {
    const parser = this.parser;
    const content = fs.readFileSync(file.path, enc);
    let count = 0;

    parser.parseFuncFromString(
      content,
      {
        list: options.func.list,
      },
      (key, options) => {
        parser.set(key, Object.assign({}, options));
        ++count;
      },
    );

    if (count > 0) {
      console.log(`i18next-scanner: count=${chalk.cyan(count)}, file=${chalk.yellow(JSON.stringify(file.relative))}`);
    }
    done();
  },
};
