import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {Typography, CardContent, Card, ButtonBase } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Style from './Style';

class CardButton extends React.Component {

  render() {
    const { classes, CardName, CardDescription } = this.props;

    return (
      <ButtonBase className={classes.center}>
        <Card className={classes.card}>
          <CardContent>
            <div className={classes.center}>
              <FontAwesomeIcon  className={classes.icon} icon="graduation-cap"></FontAwesomeIcon>
            </div>
            <Typography variant="h6" className={classes.typography} >
              {CardName}
            </Typography>
            <Typography  paragraph={true} className={classes.paragraph}> 
              {CardDescription}
            </Typography>       
          </CardContent>
        </Card>
      </ButtonBase>
    );
  }
}

CardButton.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(CardButton);
