import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Avatar, CardHeader} from '@material-ui/core';
import IconButton from '@material-ui/core/IconButton';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Style from './Style';

import imageUrl from '../../images/deneme.jpg'

class TrainerCard extends React.Component {

  render() {
    const { classes, trainerName } = this.props;

    return (
      <div className={classes.trainerCard}>
        <CardHeader
          avatar={
            <Avatar src={imageUrl}> 
            </Avatar>
          }
          action={
            <IconButton>
              <ExpandMoreIcon />
            </IconButton>
          }
          title={trainerName}
          subheader="Eğitmen"
        />
      </div>
    );
  }
}

export default withStyles(Style)(TrainerCard);