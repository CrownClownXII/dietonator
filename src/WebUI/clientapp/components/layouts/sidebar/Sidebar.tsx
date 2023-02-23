import Link from "next/link";
import { useEffect, useMemo, useState } from "react";
import SidebarItem, { ISidebarItem } from "./sidebar-item/SidebarItem";
import Logo from "./logo/Logo";
import SidebarItemContainer from "./sidebar-item-container/SidebarItemContainer";
import Separator from "../../separators/Separator";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../../store/store";
import { NextRouter, useRouter } from "next/router";
import { closeSidebar } from "../../../store/slices/layout/layoutSlice";
import LoginBtn from "../../auth/LoginBtn";

export interface ISidebar {}

const Sidebar = ({}: ISidebar): JSX.Element => {
  const router: NextRouter = useRouter();
  const dispatch: AppDispatch = useDispatch<AppDispatch>();

  const open: boolean = useSelector(
    (state: RootState) => state.layout.sidebarOpen
  );

  const handleSidebarClose: () => void = () => {
    dispatch(closeSidebar());
  };

  useEffect(() => {
    router.events.on("routeChangeComplete", handleSidebarClose);

    return () => {
      router.events.off("routeChangeComplete", handleSidebarClose);
    };
  }, [router.events]);

  const sidebarItems: ISidebarItem[] = useMemo(
    () => [
      { href: "/", label: "Dashboard" },
      { href: "/calendar", label: "Calendar" },
      { href: "/plans", label: "Plans" },
      { href: "/c", label: "Dashboard" },
    ],
    []
  );

  return (
    <nav
      className={`w-screen lg:w-64 bg-gray-50 dark:bg-gray-800 overflow-y-auto py-4 px-3 ${
        open && "h-screen"
      }`}
    >
      <Logo />
      <div className={`space-y-2 mt-5 md:block ${!open && "hidden"}`}>
        <SidebarItemContainer items={sidebarItems} />
        <Separator />
        <SidebarItem href="/logout" label="Logout" />
        <LoginBtn />
      </div>
    </nav>
  );
};

export default Sidebar;
