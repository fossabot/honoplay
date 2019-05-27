import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {Avatar, CardHeader, IconButton} from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { Style } from './Style';

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
          subheader={translate('Trainer')}
        />
      </div>
    );
  }
}

export default withStyles(Style)(TrainerCard);