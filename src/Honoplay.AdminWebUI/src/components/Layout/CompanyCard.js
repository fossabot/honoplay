import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {CardHeader, CardMedia} from '@material-ui/core';
import { Style } from './Style';

import imageUrl from '../../images/deneme.jpg';

class CompanyCard extends React.Component {
  
  render() {
    const { classes, companyName } = this.props;
    return (
      <div className={classes.companyCard}>
        <CardHeader
          avatar={
            <CardMedia
                className={classes.cardMedia}
                component="img"
                src={imageUrl}
            />
          }
          title={companyName}
        />
      </div>
    );
  }
}

export default withStyles(Style)(CompanyCard);