/// <binding />
"use strict";

var gulp = require("gulp"),
    sass = require("gulp-sass")(require("sass")),
    npmdist = require('gulp-npm-dist'),
    autoprefixer = require("gulp-autoprefixer"),
    sourcemaps = require("gulp-sourcemaps"),
    cleancss = require('gulp-clean-css'),
    rename = require('gulp-rename'),
    del = require('del');

const paths = {
    base: {
        base: {
            dir: ''
        },
        node: {
            dir: 'node_modules'
        },
        packageLock: {
            files: 'package-lock.json'
        }
    },
    dist: {
        base: {
            dir: "wwwroot/dist",
            files: "wwwroot/dist/**/*"
        },
        libs: {
            dir: "wwwroot/dist/libs"
        },
        css: {
            landing: "wwwroot/dist/landing/css",
            dashboard: "wwwroot/dist/dashboard/css",
            custom: "wwwroot/dist/css",
        },
        js: {
            dir: "wwwroot/dist/js",
            files: "wwwroot/dist/js/pages",
            landing: "wwwroot/dist/landing/js",
            dashboard: "wwwroot/dist/dashboard/js",
        },
    },
    src: {
        base: {
            dir: "wwwroot/assets",
            files: "wwwroot/assets/**/*"
        },
        css: {
            dir: "wwwroot/assets/css",
            files: "wwwroot/assets/css/**/*",
            main: "wwwroot/assets/css/*.scss"
        },
        html: {
            dir: "wwwroot/assets",
            files: "wwwroot/assets/**/*.html",
        },
        img: {
            dir: "wwwroot/assets/images",
            files: "wwwroot/assets/**/*",
        },
        js: {
            dir: "wwwroot/assets/js",
            pages: "wwwroot/assets/js/pages",
            files: "wwwroot/assets/js/pages/*.js",
            main: "wwwroot/assets/js/*.js",
            landing: "wwwroot/assets/landing/js/*.js",
            dashboard: "wwwroot/assets/dashboard/js/*.js",
        },
        partials: {
            dir: "wwwroot/assets/partials",
            files: "wwwroot/assets/partials/**/*"
        },
        scss: {
            landing: {
                dir: "wwwroot/assets/landing/scss",
                files: "wwwroot/assets/landing/scss/**/*",
                main: "wwwroot/assets/landing/scss/*.scss"
            },
            dashboard: {
                dir: "wwwroot/assets/dashboard/scss",
                files: "wwwroot/assets/dashboard/scss/**/*",
                main: "wwwroot/assets/dashboard/scss/*.scss"
            }
        }
    }
};

gulp.task("watch", function () {
    gulp.watch(paths.src.scss.files, 'scss');
    gulp.watch(paths.src.js.dir, 'js');
});

gulp.task("scss:landing", function () {
    return gulp
        .src(paths.src.scss.landing.main)
        .pipe(sourcemaps.init())
        .pipe(sass.sync().on('error', sass.logError))
        .pipe(autoprefixer())
        //.pipe(gulp.dest(paths.dist.css.landing))
        .pipe(cleancss())
        .pipe(rename({ suffix: ".min" }))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest(paths.dist.css.landing));

});

gulp.task("scss:dashboard", function () {
    return gulp
        .src(paths.src.scss.dashboard.main)
        .pipe(sourcemaps.init())
        .pipe(sass.sync().on('error', sass.logError))
        .pipe(autoprefixer())
        //.pipe(gulp.dest(paths.dist.css.dashboard))
        .pipe(cleancss())
        .pipe(rename({ suffix: ".min" }))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest(paths.dist.css.dashboard));

});

gulp.task("scss:custom", function () {
    return gulp
        .src(paths.src.css.main)
        .pipe(sourcemaps.init())
        .pipe(sass.sync().on('error', sass.logError))
        .pipe(autoprefixer())
        //.pipe(gulp.dest(paths.dist.css.landing))
        .pipe(cleancss())
        .pipe(rename({ suffix: ".min" }))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest(paths.dist.css.custom));

});

gulp.task('js', function () {
    return gulp
        .src(paths.src.js.main)
        .pipe(gulp.dest(paths.dist.js.dir));
});

gulp.task('js:landing', function () {
    return gulp
        .src(paths.src.js.landing)
        .pipe(gulp.dest(paths.dist.js.landing));
});

gulp.task('js:dashboard', function () {
    return gulp
        .src(paths.src.js.dashboard)
        .pipe(gulp.dest(paths.dist.js.dashboard));
});

gulp.task('copy:libs', function () {
    return gulp
        .src(npmdist(), { base: paths.base.node.dir })
        .pipe(rename(function (path) {
            path.dirname = path.dirname.replace(/\/dist/, '').replace(/\\dist/, '');
        }))
        .pipe(gulp.dest(paths.dist.libs.dir));
});

gulp.task('clean:packageLock', function (callback) {
    del.sync(paths.base.packageLock.files);
    callback();
});

gulp.task('clean:dist', function (callback) {
    del.sync(paths.dist.base.dir);
    callback();
});

gulp.task('build',
    gulp.series(
        gulp.parallel(
            'clean:packageLock',
            'clean:dist',
            //'copy:all',
            'copy:libs'),
        //'fileinclude',
        'js',
        'js:landing',
        'js:dashboard',
        'scss:landing',
        'scss:dashboard',
        'scss:custom'));
