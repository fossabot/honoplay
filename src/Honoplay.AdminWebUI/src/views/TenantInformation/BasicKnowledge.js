import React from "react";
import { translate } from "@omegabigdata/terasu-api-proxy";
import { withStyles } from "@material-ui/core/styles";
import { Grid } from "@material-ui/core";
import Style from "../Style";
import Input from "../../components/Input/InputTextComponent";
import ImageInput from "../../components/Input/ImageInputComponent";

class BasicKnowledge extends React.Component {
  constructor(props) {
    super(props);
  }

  tenantModel = {
    name: null,
    description: null,
    logo: null,
    hostName:
      Math.random()
        .toString(36)
        .substring(2) + new Date().getTime().toString(36)
  };

  render() {
    const { classes, isError } = this.props;
    return (
      <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error = {isError}
              onChange={value => {
                this.tenantModel.name = value;
                this.props.basicTenantModel(this.tenantModel);
              }}
              labelName={translate("TenantName")}
              inputType="text"
              name="TenantName"
            />
            <Input
              error = {isError}
              onChange={value => {
                this.tenantModel.description = value;
                this.props.basicTenantModel(this.tenantModel);
              }}
              labelName={translate("Description")}
              multiline
              inputType="text"
              name="Description"
            />
            <ImageInput
              selectedImage={value => {
                this.tenantModel.logo = value;
                this.props.basicTenantModel(this.tenantModel);
              }}
              labelName={translate("TenantLogo")}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(BasicKnowledge);
