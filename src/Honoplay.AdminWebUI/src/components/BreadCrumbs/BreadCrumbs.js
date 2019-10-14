import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { NavLink as RouterLink } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import { Style } from './Style';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import withBreadcrumbs from 'react-router-breadcrumbs-hoc';

const Link = React.forwardRef((props, ref) => (
  <RouterLink {...props} innerRef={ref} />
));

function PureBreadcrumbs(props) {
  const { classes } = props;
  return (
    <Breadcrumbs>
      <Link to="/trainingseries" className={classes.text}>
        {translate('TrainingSeries')}
      </Link>
      {props.match.params.trainingSeriesId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingSeriesId}`}
          className={classes.text}
        >
          {localStorage.getItem('trainingSeriesName')}
        </Link>
      )}
      {props.match.params.trainingSeriesId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingId}/training`}
          className={classes.text}
        >
          {translate('Trainings')}
        </Link>
      )}
      {props.match.params.trainingId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingSeriesId}/training/${props.match.params.trainingId}`}
          className={classes.text}
        >
          {localStorage.getItem('trainingName')}
        </Link>
      )}
      {props.match.params.trainingId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingId}/training/${props.match.params.trainingId}/classroom`}
          className={classes.text}
        >
          {translate('Classrooms')}
        </Link>
      )}
      {props.match.params.classroomId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingId}/training/${props.match.params.trainingId}/classroom/${props.match.params.classroomId}`}
          className={classes.text}
        >
          {localStorage.getItem('classroomName')}
        </Link>
      )}
      {props.match.params.classroomId && (
        <Link
          to={`/trainingseries/${props.match.params.trainingId}/training/${props.match.params.trainingId}/classroom/${props.match.params.classroomId}/session`}
          className={classes.text}
        >
          {translate('Sessions')}
        </Link>
      )}
    </Breadcrumbs>
  );
}
export default withStyles(Style)(withBreadcrumbs()(PureBreadcrumbs));
