declare module 'aurelia-bootstrapper/index' {
    import Promise = dojo.promise.Promise;

    export function bootstrap(configure: any): Promise<{}>;

}

declare module 'aurelia-bootstrapper' {
	export * from 'aurelia-bootstrapper/index';
}
