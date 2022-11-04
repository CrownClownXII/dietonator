import { createApi } from "@reduxjs/toolkit/query/react";
import { Meal, MealTypeEnum } from "../../../model/Meal";
import { Product } from "../../../model/Product";
import {
  DailyUserStatistic,
  DailyUserStatisticResponse,
  UserStatistic,
  UserStatisticResponse,
} from "../../../model/UserStatistic";
import { axiosBaseQuery } from "../../services/axiosBaseQuery";

export interface CreateMealCommand {
  name: string;
  type: MealTypeEnum;
  forDate: string;
}

export interface CreateMealProductCommand {
  mealId: string;
  productId: string;
  amount: number;
}

export interface UpdateMealProductCommand {
  mealId: string;
  mealProductId: string;
  amount: number;
}

export interface GetMealPlansQuery {
  dateFrom: string;
  dateTo: string;
}

export interface GetDailyUserStatisticQuery {
  date: string;
}

export interface GetProductListQuery {
  searchBy?: string | null;
}

export const mealApi = createApi({
  reducerPath: "meal",
  tagTypes: ["Meal", "Product"],
  baseQuery: axiosBaseQuery({ baseUrl: "http://localhost:5000/api/" }),
  endpoints: (builder) => ({
    getProducts: builder.query<Product[], GetProductListQuery>({
      query: () => ({ url: "product", method: "GET" }),
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({ type: "Product", id } as const)),
              { type: "Product", id: "LIST" },
            ]
          : [{ type: "Product", id: "LIST" }],
    }),
    getMeals: builder.query<Meal[], void>({
      query: () => ({ url: "meal/fordate", method: "GET" }),
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({ type: "Meal", id } as const)),
              { type: "Meal", id: "LIST" },
            ]
          : [{ type: "Meal", id: "LIST" }],
    }),
    getMealPlans: builder.query<UserStatistic[], GetMealPlansQuery>({
      query: (query) => ({
        url: `statistic/user?DateFrom=${query.dateFrom}&DateTo=${query.dateTo}`,
        method: "GET",
      }),
      providesTags: () => [{ type: "Meal", id: "PLAN" }],
      transformResponse: (response: UserStatisticResponse[]) => {
        return response.map((c) => ({
          userId: c.userId,
          forDate: c.forDate,
          caloricInfo: {
            kcal: c.kcal,
            fats: c.fats,
            proteins: c.proteins,
            carbohydrates: c.carbohydrates,
          },
        }));
      },
    }),
    getDailyMealPlan: builder.query<
      DailyUserStatistic,
      GetDailyUserStatisticQuery
    >({
      query: (query) => ({
        url: `statistic/details?Date=${query.date}`,
        method: "GET",
      }),
      providesTags: () => [{ type: "Meal", id: "PLAN" }],
      transformResponse: (response: DailyUserStatisticResponse) => {
        const result: DailyUserStatistic = {
          userId: response.userId,
          forDate: response.forDate,
          caloricInfo: {
            kcal: response.kcal,
            fats: response.fats,
            proteins: response.proteins,
            carbohydrates: response.carbohydrates,
          },
          meals: response.meals.map((c) => ({
            id: c.id,
            caloricInfo: {
              kcal: c.kcal,
              fats: c.fats,
              proteins: c.proteins,
              carbohydrates: c.carbohydrates,
            },
            products: c.products.map((x) => ({
              id: x.id,
              mealId: x.mealId,
              productId: x.productId,
              name: x.name,
              amount: x.amount,
              kcal: x.kcal,
              fats: x.fats,
              proteins: x.proteins,
              carbohydrates: x.carbohydrates,
            })),
          })),
        };
        return result;
      },
    }),
    addMeal: builder.mutation<string, Partial<CreateMealCommand>>({
      query(data) {
        return { url: `meal`, method: "POST", data };
      },
      invalidatesTags: [{ type: "Meal", id: "PLAN" }],
    }),
    addMealProduct: builder.mutation<string, Partial<CreateMealProductCommand>>(
      {
        query(data) {
          return { url: `meal/${data.mealId}/product`, method: "POST", data };
        },
        invalidatesTags: [{ type: "Meal", id: "PLAN" }],
      }
    ),
    updateMealProduct: builder.mutation<
      void,
      Partial<UpdateMealProductCommand>
    >({
      query(data) {
        return {
          url: `meal/${data.mealId}/product/${data.mealProductId}`,
          method: "PUT",
          data,
        };
      },
      invalidatesTags: [{ type: "Meal", id: "PLAN" }],
    }),
  }),
});

export const {
  useAddMealMutation,
  useGetMealsQuery,
  useGetMealPlansQuery,
  useGetDailyMealPlanQuery,
  useAddMealProductMutation,
  useGetProductsQuery,
  useUpdateMealProductMutation,
} = mealApi;
