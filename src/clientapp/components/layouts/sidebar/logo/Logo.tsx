import Link from "next/link";
import { useDispatch } from "react-redux";
import { toggleSidebar } from "../../../../store/slices/layout/layoutSlice";
import { AppDispatch } from "../../../../store/store";

export interface ILogo {}

const Logo = ({}: ILogo): JSX.Element => {
  const dispatch: AppDispatch = useDispatch<AppDispatch>();

  const handleToggleSidebar: () => void = () => {
    dispatch(toggleSidebar());
  };

  return (
    <Link href="/" className="flex">
      <div className="flex justify-between items-center px-2.5 my-2.5">
        <div className="flex items-center">
          <img
            src="https://flowbite.com/docs/images/logo.svg"
            className="mr-3 h-6 sm:h-7"
            alt="Flowbite Logo"
          />
          <span className="self-center text-xl font-semibold whitespace-nowrap dark:text-white">
            Dietonator
          </span>
        </div>
        <button
          onClick={handleToggleSidebar}
          className="w-8 h-5 hover:scale-110 duration-100 cursor-pointer lg:hidden"
        >
          <div className="border-t-2 border-gray-200 dark:border-gray-700"></div>
          <div className="mt-2 border-t-2 border-gray-200 dark:border-gray-700"></div>
          <div className="mt-2 border-t-2 border-gray-200 dark:border-gray-700"></div>
        </button>
      </div>
    </Link>
  );
};

export default Logo;
