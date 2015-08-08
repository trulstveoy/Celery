declare module Data {
    
    export interface Food {
        name: string;
        id: string;
        calories: string;
        ediblePartPercent: string;
        kiloJoules:string;
        waterGrams: string;
        mainCategory: MainCategory;
        subCategory: SubCategory;
    }

    export interface MainCategory {
        name:string;
    }

    export interface SubCategory {
        name:string;
    }

    export interface User {
        id: string;
        externalId: string;
        firstName: string;
        lastName: string;
        providerName: string;
        bearerToken: string;
    }
}