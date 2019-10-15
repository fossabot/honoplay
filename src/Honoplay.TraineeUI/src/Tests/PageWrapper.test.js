import React from "react";
import "should";
import "should-enzyme";
import sinon from "sinon";
import { configure, shallow, mount, render } from "enzyme";
import PageWrapper from "../Containers/PageWrapper";
import { expect } from "chai";

import Adapter from "enzyme-adapter-react-16";
configure({ adapter: new Adapter() });

describe("<PageWrapper> {children} </PageWrapper>", () => {
  it("should have props for data, boxNumber and children ", () => {
    const wrapper = mount(<PageWrapper boxNumber="4" children={"<a />"} />);
    wrapper.should.have.prop("boxNumber").defined;
    wrapper.should.have.prop("children").defined;
  });
});
