import type { AppProps } from "next/app";
import { ReactElement, ReactNode } from "react";
import { Provider } from "react-redux";
import { store } from "../store/store";
import "./globals.css";
import { NextPageWithLayout } from "./page";

interface IAppPropsWithLayout extends AppProps {
  Component: NextPageWithLayout;
}

function MyApp({ Component, pageProps }: IAppPropsWithLayout) {
  const getLayout: (_page: ReactElement) => ReactNode =
    Component.getLayout || ((page) => page);

  return (
    <Provider store={store}>{getLayout(<Component {...pageProps} />)}</Provider>
  );
}

export default MyApp;
