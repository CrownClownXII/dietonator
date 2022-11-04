import { CaloricInfo } from "./Common";
import { MealProduct } from "./Meal";

export interface UserStatisticResponse {
    userId: string;
    forDate: string; 
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrates: number;
}

export interface UserStatistic {
    userId: string;
    forDate: string;
    caloricInfo: CaloricInfo;
}

export interface DailyUserStatisticResponse {
    userId: string;
    forDate: string;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrates: number;
    meals: MealStatisticResponse[];
}

export interface MealStatisticResponse {
    id: string;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrates: number;
    products: ProductStatisticResponse[];
}

export interface ProductStatisticResponse {
    id: string;
    mealId: string;
    productId: string;
    name: string;
    amount: number;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrates: number;
}

export interface DailyUserStatistic {
    userId: string;
    forDate: string;
    caloricInfo: CaloricInfo;
    meals: MealStatistic[];
}

export interface MealStatistic {
    id: string;
    caloricInfo: CaloricInfo;
    products: MealProduct[];
}

export interface ProductStatistic {
    id: string;
    name: string;
    amount: number;
    caloricInfo: CaloricInfo;
}
