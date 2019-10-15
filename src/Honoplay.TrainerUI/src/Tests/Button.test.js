import React from "react";
import PropTypes from "prop-types";
// import should from "should";
import "should";
import "should-enzyme";
import sinon from "sinon";
import { configure, shallow, mount, render } from "enzyme";
import { Button } from "../Components/Button";
import { expect } from "chai";

import Adapter from "enzyme-adapter-react-16";
configure({ adapter: new Adapter() });

describe("<Button />", () => {
  it("should have props for data, title, color, onClick ", () => {
    const onClick = sinon.spy();
    const wrapper = mount(
      <Button title={"selam"} color="success" onClick={onClick} className="a" />
    );

    // wrapper.should.have.prop("title", "selam");
    wrapper.should.have.prop("title").defined;
    wrapper.should.have.prop("color").defined;
    wrapper.should.have.prop("onClick").defined;
  });

  it('should invoke onClick callback when click to "Button"', () => {
    const onClick = sinon.spy();
    const wrapper = shallow(
      <Button title={"selam"} color="success" onClick={onClick} />
    );
    wrapper
      .find("button")
      .shallow()
      .simulate("click");
    expect(onClick.called).to.equal(true);
  });
});
