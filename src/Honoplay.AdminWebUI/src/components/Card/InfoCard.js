import React, { Children } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Card, CardContent, CardHeader, IconButton } from '@material-ui/core';
import { Style } from './Style';

class Summary extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { classes, children, titleName, icon } = this.props;

    return (
      <Card className={classes.infoCard}>
        <CardHeader
          title={titleName}
          avatar={icon && <IconButton>{icon}</IconButton>}
        ></CardHeader>
        <CardContent>{children}</CardContent>
      </Card>
    );
  }
}

export default withStyles(Style)(Summary);
