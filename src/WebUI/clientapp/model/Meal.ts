export enum MealTypeEnum {
    calculableMeal = 0,
    template = 1
}

export interface Meal {
    id: string;
    name: string;
    type: MealTypeEnum;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrate: number;
}

export interface MealProduct {
    id: string;
    mealId: string;
    productId: string;
    name: string;
    amount:number;
    kcal: number;
    proteins: number;
    fats: number;
    carbohydrates: number;
  }