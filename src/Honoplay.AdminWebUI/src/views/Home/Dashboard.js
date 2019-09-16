import React from 'react';
import { withStyles } from '@material-ui/core/styles';

import Style from '../Style';

class Dashboard extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <center>
          <h2 style={{ color: '#bdbdbd', paddingTop: 300 }}>Dashboard*</h2>
        </center>
      </div>
    );
  }
}

export default withStyles(Style)(Dashboard);
