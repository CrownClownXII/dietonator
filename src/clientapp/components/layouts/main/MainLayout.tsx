import Head from "next/head";

export interface IMainLayout {
  children: JSX.Element;
}

const MainLayout = ({ children }: IMainLayout): JSX.Element => {
  return (
    <>
      <Head>
        <title>Primary Layout Example</title>
      </Head>
      <main className="bg-white dark:bg-gray-900 dark:text-gray-200 flex flex-col lg:flex-row min-h-screen">
        {children}
      </main>
    </>
  );
};

export default MainLayout;
