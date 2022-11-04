import { Action, configureStore, ThunkAction } from "@reduxjs/toolkit";
import layoutReducer from "./slices/layout/layoutSlice";
import { mealApi } from "./slices/meal/mealSlice";

export const store = configureStore({
  reducer: {
    layout: layoutReducer,
    [mealApi.reducerPath]: mealApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(mealApi.middleware),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
